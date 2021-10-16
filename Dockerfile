#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BloodLoop.WebApi/BloodLoop.WebApi.csproj", "BloodLoop.WebApi/"]
COPY ["BloodLoop.Application/BloodLoop.Application.csproj", "BloodLoop.Application/"]
COPY ["BloodLoop.Domain/BloodLoop.Domain.csproj", "BloodLoop.Domain/"]
COPY ["BloodCore/BloodCore.csproj", "BloodCore/"]
COPY ["BloodLoop.Infrastructure/BloodLoop.Infrastructure.csproj", "BloodLoop.Infrastructure/"]
RUN dotnet restore "BloodLoop.WebApi/BloodLoop.WebApi.csproj"
COPY . .
WORKDIR "/src/BloodLoop.WebApi"
RUN dotnet build "BloodLoop.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BloodLoop.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BloodLoop.WebApi.dll"]