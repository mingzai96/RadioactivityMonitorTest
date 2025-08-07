@echo off

echo Restoring project dependencies...
dotnet restore

echo Building the project...
dotnet build

echo Running tests...
dotnet test --no-build --verbosity normal

pause