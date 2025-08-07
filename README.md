# RadioactivityMonitor

A simple console application to simulate monitoring the radioactivity levels and trigger an alarm if values fall outside the safe range (17-21).

## Requirements

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- .NET CLI (dotnet) available in your terminal

## How to Build

Open a terminal in the solution root and run:

```sh
dotnet restore
dotnet build
```

## How to Run

You can run run.bat

or

Run the console application:

```sh
dotnet run --project src/RadioactivityMonitor
```

You will see output like:

```
AlarmOn: False, AlarmCount: 0
AlarmOn: True, AlarmCount: 1
...
```

### Run with Docker
1. Build the Docker image:

```sh
docker build -t radioactivity-monitor .
```

2. Run the container:

```sh
docker run --rm radioactivity-monitor
```

This will start the application inside a Docker container and print the alarm status to the console.

## How to Test

You can run the run-tests.bat

or

Navigate to the test project folder and run tests:

```sh
dotnet test
```

Test results will be displayed in the terminal.

