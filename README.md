# TO INSTALL
1) Install dotnet and check with :  dotnet --version
2) Go into projet and install these NuGet packages
    - dotnet add package Microsoft.EntityFrameworkCore
    - dotnet add package Microsoft.EntityFrameworkCore.InMemory
    - dotnet add package Microsoft.EntityFrameworkCore.Design
    - dotnet add package AutoMapper --version 10.1.1
    - dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 8.1.1
    - Motnet add package  Microsoft.EntityFrameworkCore.SqlServer

3) Launch VS Code Quick Open (Ctrl+P), paste the following command, and press enter : 
```ext install temilaj.asp-net-core-vs-code-extension-pack```

If error with omnisharp on ubuntu add a symlink : 
https://github.com/OmniSharp/omnisharp-vscode/issues/4201

# TO RUN
dotnet run

# TO DO

- Connect to db
- Authentification(jwt, google, facebook)
- Add logging Services
- Add configuration files to get the constants


# Architecture
 N-Tier architecture(AKA Layered Architecture) is used(separation of concern) : 
 Each of these layers should be Single Responsibility to avoid tight coupling and to support Separation of Concern.
 
 Client ===> PRESENTATION LAYER(Controllers) ===> BUSINESS LOGIC LAYER(Services) ===> DATA ACCES LAYER(Repositories) ===> Database(Context)

 # Database configuration 

 you can use Db in memory ( see startup) or config your own db localy or in the cloud. I use Microsoft sql server is used.

 - https://docs.microsoft.com/fr-fr/sql/linux/quickstart-install-connect-ubuntu?view=sql-server-ver15

`systemctl status mssql-server --no-pager` to see if microsoft sql server is launch

## EF migration 

-  dotnet tool install --global dotnet-ef
- dotnet tool update --global dotnet-ef
- 


