set MAIN_PROJ=AppMain

:: --------------------------------------------------

:: RD /S /Q "%MAIN_PROJ%\obj\Release"
:: RD /S /Q "%MAIN_PROJ%\bin\Release"

dotnet publish %MAIN_PROJ%\%MAIN_PROJ%.csproj -f:net6.0-android -c:Release ^
/p:ApplicationId=de.noname.familyexpenseslist /p:AndroidPackageFormat=apk /p:AndroidKeyStore=False


