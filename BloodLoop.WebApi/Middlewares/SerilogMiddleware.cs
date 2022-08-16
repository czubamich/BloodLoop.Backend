using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Org.BouncyCastle.Asn1.Ocsp;
using Serilog.Context;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

class SerilogMiddleware
{
    const string MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

    static readonly Serilog.ILogger Log = Serilog.Log.ForContext<SerilogMiddleware>();

    static readonly HashSet<string> HeaderWhitelist = new() { "Content-Type", "Content-Length", "User-Agent" };

    readonly RequestDelegate _next;

    private static string[] ExcludedPaths = new[]
    {
        "Auth",
        "Accounts"
    };

    private static string[] LowLevelPaths = new[]
    {
        "Dictionaries"
    };

    public SerilogMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    // ReSharper disable once UnusedMember.Global
    public async Task Invoke(HttpContext httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        var requestBodyPayload = string.Empty;
        var responseBodyPayload = string.Empty;
        var template = MessageTemplate;

        var start = Stopwatch.GetTimestamp();
        var sessionId = httpContext.Request.Headers.FirstOrDefault(x => x.Key == "SessionId").Value.ToString();
        using (LogContext.PushProperty("SessionId", sessionId ?? "none"))

        try
        {
            if (ExcludedPaths.Any(p => GetPath(httpContext).Contains(p)))
            {
                await _next(httpContext);
                return;
            }
            else
            {
                var responseBody = await HandleNext(_next, httpContext);
                var elapsedMs = GetElapsedMilliseconds(start, Stopwatch.GetTimestamp());

                var headers = ReadRequestHeaders(httpContext.Request);
                LogContext.PushProperty("RequestHeaders", headers);

                if (httpContext.Request.Query.Any())
                {
                    var queryParams = string.Join('\n', httpContext.Request.Query.Select(q => $"{q.Key}={string.Join(", ", q.Value)}"));
                    if (string.IsNullOrWhiteSpace(queryParams) == false)
                    {
                        LogContext.PushProperty("QueryParams", queryParams);
                    }
                }
                if (httpContext.Request.Path != PathString.Empty
                    && !LowLevelPaths.Any(p => GetPath(httpContext).Contains(p)))
                {
                    requestBodyPayload = await ReadRequestBody(httpContext.Request);
                    if (string.IsNullOrWhiteSpace(requestBodyPayload))
                        LogContext.PushProperty("RequestBody", requestBodyPayload);
                }

                var statusCode = httpContext.Response?.StatusCode;

                var level = statusCode > 499 ? LogEventLevel.Error : LogEventLevel.Information;

                LogContext.PushProperty("ResponseBody", responseBody);

                var log = level == LogEventLevel.Error ? LogForErrorContext(httpContext) : Log;
                log.Write(level, MessageTemplate, httpContext.Request.Method, GetPath(httpContext), statusCode, elapsedMs);
            }
        }
        catch (Exception ex) when (LogException(httpContext, template, GetElapsedMilliseconds(start, Stopwatch.GetTimestamp()), ex)) { }
    }

    private async Task<string> HandleNext(RequestDelegate next, HttpContext httpContext)
    {
        using (var swapStream = new MemoryStream())
        {
            var originalResponseBody = httpContext.Response.Body;

            httpContext.Response.Body = swapStream;

            await _next(httpContext);

            swapStream.Seek(0, SeekOrigin.Begin);
            string responseBody = new StreamReader(swapStream).ReadToEnd();
            swapStream.Seek(0, SeekOrigin.Begin);

            await swapStream.CopyToAsync(originalResponseBody);
            httpContext.Response.Body = originalResponseBody;

            return responseBody;
        }
    }

    private static async Task<string> ReadRequestBody(HttpRequest request)
    {
        request.EnableBuffering();

        using var reader = new StreamReader(request.Body, leaveOpen: true);
        reader.BaseStream.Seek(0, SeekOrigin.Begin);
        var body = await reader.ReadToEndAsync();
        request.Body.Position = 0;

        return body;
    }

    private static string ReadRequestHeaders(HttpRequest request)
    {
        return string.Join("\r\n",
            request.Headers
                .Where(x => HeaderWhitelist.Contains(x.Key))
                .Select(x => $"{x.Key}:{x.Value}").ToArray()
            );
    }

    static bool LogException(HttpContext httpContext, string messageTemplate, double elapsedMs, Exception ex)
    {
        LogForErrorContext(httpContext)
            .Error(ex, messageTemplate, httpContext.Request.Method, GetPath(httpContext), 500, elapsedMs);

        return false;
    }

    static Serilog.ILogger LogForErrorContext(HttpContext httpContext)
    {
        var request = httpContext.Request;

        var loggedHeaders = request.Headers
            .Where(h => HeaderWhitelist.Contains(h.Key))
            .ToDictionary(h => h.Key, h => h.Value.ToString());

        var result = Log
            .ForContext("RequestHeaders", loggedHeaders, destructureObjects: true)
            .ForContext("RequestHost", request.Host)
            .ForContext("RequestProtocol", request.Protocol);

        return result;
    }

    static double GetElapsedMilliseconds(long start, long stop)
    {
        return (stop - start) * 1000 / (double)Stopwatch.Frequency;
    }

    static string GetPath(HttpContext httpContext)
    {
        return httpContext.Features.Get<IHttpRequestFeature>()?.RawTarget ?? httpContext.Request.Path.ToString();
    }
}
