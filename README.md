# FamilyExpensesListApp
This Project is the desktop- or mobil-app alternative for the "FamilyExpensesList"

Its use the same (remote-)[backend](https://github.com/Oigty81/family-expenses-list-backend) for use user authentication and persistence data management.
Its also use the same [frontend](https://github.com/Oigty81/family-expenses-list-frontend) with an another configuration as GUI in a webview-component.

The Project use .NET MAUI as cross-compiler thus it runs on Windows, Android and iOS Devices. 


#
## Project-State:
in Progress 
 
### Todo

- [ ] added controller for routes: user, category and expenses
- [ ] added config-data-provider for common app
- [ ] added optionally basic authentication service for local TCP/HTTP-Channel
- [ ] added HTTP-Client for communication with remote-backend
- [ ] implement dynamic free-port-search for Webservicehost
- [ ] implement offline modus (temporary datastore with SQLite and Data-Merger)
- [ ] added unit tests for all classes

# 
## Project setup

### Prerequisite

- [Visual Studio 2022](https://visualstudio.microsoft.com/de/downloads/)
- [PHP](https://www.php.net/downloads.php) (to run the script for link the frontend-bundle-files to the Web.Resources project)

### Install the repository
```
git clone https://github.com/Oigty81/FamilyExpensesListApp.git
```

### Build-Scripts
- `BUILD_WINDOWS.bat` to build a Windows-Desktop-App for Any-CPU
- `BUILD_ANDROID_WITHOUT_KEYSTORE.bat` to build a Android APK-File without a keystore signature

### Frontend Build and Setup
comming soon....