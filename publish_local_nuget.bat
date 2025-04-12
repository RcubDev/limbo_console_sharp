REM This batch file automates building and placing local changes in the Limbo.Console.Sharp project and its demo project.
@echo off
if "%1" == "" (
    echo No version provided using default version 0.0.1
    set nuget_version="0.0.1-beta"
) else (
    set nuget_version=%1
)

REM We need to bust the nuget cache or else we will continually have to increment version numbers
REM we only really want to increment version numbers when we are ready to publish
echo This will clear your nuget cache completely if you proceed -- packages will be restored during this process
set /p continue=Continue (y/n)?
if "%continue%" == "y" (
    echo Continuing with version %nuget_version%
) else (
    echo Exiting script.
    exit /b
)

REM Navigate to the first folder and run a dotnet command
echo Changing directories to Limbo.Console.Sharp
cd Limbo.Console.Sharp

dotnet restore
echo building Limbo.Console.Sharp version %nuget_version%
dotnet build --configuration Release --no-restore
echo packing Limbo.Console.Sharp version %nuget_version%
dotnet pack --configuration Release --output ../demo/nuget
echo nuget pack completed

REM Add the local NuGet source
cd ../demo
echo Clearing nuget cache (global, http, temp)
dotnet nuget locals all --clear

dotnet add package LimboConsole.Sharp --version %nuget_version%
dotnet restore
dotnet build

echo you should restart your IDEs to get the updated code for the package
echo Done!