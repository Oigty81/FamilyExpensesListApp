SET MAIN_PROJ=AppMain

:: --------------------------------------------------

:: RD /S /Q "%MAIN_PROJ%\obj\Release"
:: RD /S /Q "%MAIN_PROJ%\bin\Release"

dotnet build %MAIN_PROJ%\%MAIN_PROJ%.csproj -f:net6.0-windows10.0.19041.0 -c:Release
