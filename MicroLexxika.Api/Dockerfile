FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
ENV ASPNETCORE_ENVIRONMENT=Development

COPY ["MicroLexxika.Api/MicroLexxika.Api.csproj", "MicroLexxika.Api/"]
RUN dotnet restore "./MicroLexxika.Api/MicroLexxika.Api.csproj"
COPY . .
WORKDIR "/src/MicroLexxika.Api"
RUN dotnet build "./MicroLexxika.Api.csproj" -c ${ASPNETCORE_ENVIRONMENT} -o /app/build

FROM build AS publish
RUN dotnet publish "./MicroLexxika.Api.csproj" -c ${ASPNETCORE_ENVIRONMENT} -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroLexxika.Api.dll"]