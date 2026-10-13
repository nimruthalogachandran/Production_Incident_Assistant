# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["Production_Incident_Assistant.slnx", "./"]
COPY ["Production_Incident_Assistant.Api/Production_Incident_Assistant.Api.csproj", "Production_Incident_Assistant.Api/"]
COPY ["Production_Incident_Assistant.Core/Production_Incident_Assistant.Core.csproj", "Production_Incident_Assistant.Core/"]
COPY ["Production_Incident_Assistant.Infrastructure/Production_Incident_Assistant.Infrastructure.csproj", "Production_Incident_Assistant.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "Production_Incident_Assistant.Api/Production_Incident_Assistant.Api.csproj"

# Copy the remaining source code
COPY . .

# Build and publish the API
WORKDIR /src/Production_Incident_Assistant.Api
RUN dotnet publish "Production_Incident_Assistant.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Production_Incident_Assistant.Api.dll"]