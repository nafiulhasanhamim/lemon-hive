# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY lemonhive.csproj ./
RUN dotnet restore

COPY Program.cs ./Program.cs
COPY appsettings.json ./appsettings.json
COPY appsettings.Development.json ./appsettings.Development.json

COPY Controllers/ ./Controllers
COPY Models/ ./Models
COPY Services/ ./Services
COPY DTOs/ ./DTOs
COPY Enums/ ./Enums
COPY Extensions/ ./Extensions
COPY Interfaces/ ./Interfaces
# COPY Profiles/ ./Profiles
COPY Migrations/ ./Migrations
COPY RabbitMQ/ ./RabbitMQ
COPY data/ ./data
COPY Configurations/ ./Configurations
COPY data/ ./data
COPY data/ ./data
RUN dotnet publish -c Release -o out

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

# Create a non-root user for security
RUN adduser --disabled-password --gecos "" appuser \
    && chown -R appuser:appuser /App

USER appuser

# Copy the published application from the build stage
COPY --from=build-env /App/out .

# Expose correct port
EXPOSE 8000

ENV ASPNETCORE_URLS=http://+:8000

# Run the application
ENTRYPOINT ["dotnet", "lemonhive.dll"]
