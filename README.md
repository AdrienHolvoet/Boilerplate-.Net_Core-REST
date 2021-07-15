# Project setup
## For Linux : 
1) Install dotnet and check the install with :  dotnet --version
2) Go into the project and install these NuGet packages
    - dotnet add package Microsoft.EntityFrameworkCore
    - dotnet add package Microsoft.EntityFrameworkCore.InMemory
    - dotnet add package Microsoft.EntityFrameworkCore.Design
    - dotnet add package AutoMapper --version 10.1.1
    - dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 8.1.1
    - dotnet add package  Microsoft.EntityFrameworkCore.SqlServer

3) Launch VS Code Quick Open (Ctrl+P), paste the following command, and press enter : 
```ext install temilaj.asp-net-core-vs-code-extension-pack```

If error with omnisharp on ubuntu add a symlink : 
https://github.com/OmniSharp/omnisharp-vscode/issues/4201

## For Windows and Mac : 
Just install Visual Studio et install the package via NuGet package manager

# To run

## For Linux : 
dotnet run

## For Windows and Mac : 

Via Visual Studio

# To do
- Authentification(jwt, google, facebook)
- Add logging Services
- Add configuration files to get the constants


# Architecture
The N-Tier architecture(AKA Layered Architecture) is used (separation of concern) : 
Each of these layers should be Single Responsibility to avoid tight coupling and to support Separation of Concern.
 
Client ===> PRESENTATION LAYER(Controllers) ===> BUSINESS LOGIC LAYER(Services) ===> DATA ACCES LAYER(Repositories) ===> Database(Context)

# Database configuration 

It is possible to use database in memory (see startup) or config one database locally or in the cloud.

for Ubuntu : 
 - https://docs.microsoft.com/fr-fr/sql/linux/quickstart-install-connect-ubuntu?view=sql-server-ver15  
 `systemctl status mssql-server --no-pager` to see if microsoft sql server is launch

# EF migration 

You must configure a valid connection string in appsetting.json

- dotnet tool install --global dotnet-ef // install the tools
- dotnet ef migrations add InitialCreate // generate .SQL in Migration folder upon your models  
- dotnet ef database update // run the SQL files just created

And the migration (the db is created if it's the first time)



