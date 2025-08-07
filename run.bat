@echo off

echo Restoring project dependencies...
dotnet restore 

echo Building the project...
dotnet build

echo Running the application...
dotnet run --project src/RadioactivityMonitor