RD /S /Q ".vs"
DEL /Q AppMain\AppMain.csproj.user
:: ---
RD /S /Q "AppMain\obj"
RD /S /Q "AppMain\bin"
RD /S /Q "Backend\obj"
RD /S /Q "Backend\bin"
RD /S /Q "Web.Documentation\obj"
RD /S /Q "Web.Documentation\bin"
RD /S /Q "Web.Resources\obj"
RD /S /Q "Web.Resources\bin"
RD /S /Q "tests\BackendTests\obj"
RD /S /Q "tests\BackendTests\bin"