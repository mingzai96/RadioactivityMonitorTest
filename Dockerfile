#Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY . ./

# Restore dependencies and build the application
RUN dotnet restore
RUN dotnet build --configuration Release

# Stage 2: Publish the application
RUN dotnet publish ./src/RadioactivityMonitor/RadioactivityMonitor.csproj -c Release -o /app/publish

# Stage 3: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "RadioactivityMonitor.dll"]