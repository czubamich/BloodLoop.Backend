using BloodLoop.Application.Services;
using BloodLoop.WebApi.Extensions;
using BloodLoop.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BloodCore.AspNet;
using BloodCore.Cqrs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BloodLoop.Infrastructure.Persistance;
using BloodCore.Persistance;
using BloodLoop.Infrastructure.Identities;
using BloodLoop.Infrastructure.Settings;
using FluentValidation.AspNetCore;
using NSwag;
using NSwag.Generation.Processors.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Hangfire;
using Hangfire.SqlServer;
using BloodLoop.Infrastructure.Workers;
using BloodLoop.Infrastructure.IoC;
using Hangfire.Annotations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BloodLoop.WebApi.Middlewares;
using BloodLoop.Domain.Donors;

namespace BloodLoop.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IEnumerable<Assembly> appAssemblies = GetAssemblies();

            services.AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblies(appAssemblies);
                opt.LocalizationEnabled = false;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("BloodLoop"),
                    opt => opt.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name));
            });

            services.AddSpecification();

            AddOptions(services);

            services.AddScoped<IUnitOfWork>(x => x.GetService<ApplicationDbContext>());

            services.Configure<DataProtectionTokenProviderOptions>(options =>
                options.TokenLifespan = TimeSpan.FromHours(3));

            services.AddHttpContextAccessor();
            services.AddSingleton<IApplicationContext, ApplicationContext>();

            services.ConfigureSettings(appAssemblies.SelectMany(x => x.GetTypes()), Configuration);

            services.RegisterInjectables(appAssemblies.SelectMany(x => x.GetTypes()));

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeResolver()
                .UseSqlServerStorage(Configuration.GetConnectionString("Hangfire"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));
            services.AddHangfireServer();

            services.AddCqrs();

            services.AddLogging(config => 
                config.AddSerilog()
            );

            services.AddBloodLoopAuthentication(Configuration);

            services.AddSecurity(Configuration);

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new IdentityJsonConverter<DonorId>());
                });

            services.AddOpenApiDocument(options =>
            {
                options.DocumentName = "v1";
                options.Title = "BloodLoop Api";
                options.Version = "v1";

                options.AddSecurity("JWT", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    BearerFormat = "JWT",
                    Description = "Type into the textbox: Bearer {your JWT token}.",
                });


                options.OperationProcessors.Add(
                    new OperationSecurityScopeProcessor("JWT"));
            });
        }

        public IServiceCollection AddOptions(IServiceCollection services)
        {
            services
                .AddOptions<AuthenticationSettings>()
                .Bind(Configuration.GetSection(AuthenticationSettings.SECTION))
                .ValidateDataAnnotations();

            return services;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext, IServiceProvider sp)
        {
            
            dbContext.Database.Migrate();

            sp.SetupRecurringJobs(Configuration);

            sp.SeedRoles().ConfigureAwait(false);

            bool httpsEnabled = Configuration.Get<WebSettings>().HttpsEnabled;
            if (httpsEnabled)
            {
                app.UseHsts();
            }

            app.UseOpenApi(options => options.PostProcess = (document, _) => document.Schemes = new[] { httpsEnabled ? NSwag.OpenApiSchema.Https : NSwag.OpenApiSchema.Http });

            app.UseSwaggerUi3();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                Authorization = new[] { new HangfireDashboardJwtAuthorizationFilter(sp.GetService<IIdentityService>()) },
                IgnoreAntiforgeryToken = true,
                StatsPollingInterval = 60000 // 1 min
            });

            app.UseMiddleware<SerilogMiddleware>();

            app.UseRouting();

            app.UseCors(c =>
            {
                c.AllowAnyOrigin();
                c.AllowAnyHeader();
                c.AllowAnyMethod();
            });

            if (httpsEnabled)
                app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();
        }

        public static IEnumerable<Assembly> GetAssemblies()
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();

            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            var toLoad = referencedPaths
                .Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase))
                .Select(x => AssemblyName.GetAssemblyName(x))
                .Where(x => x.Name.Contains("Blood"))
                .ToList();

            toLoad.ForEach(x => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(x)));
            return loadedAssemblies.Where(x => x.FullName.Contains("Blood"));
        }
    }
}
