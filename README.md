# TO INSTALL
1) Install dotnet and check with :  dotnet --version
2) Go into projet and install two NuGet packages
    - dotnet add package Microsoft.EntityFrameworkCore
    - dotnet add package Microsoft.EntityFrameworkCore.InMemory
    - dotnet add package Microsoft.EntityFrameworkCore.Design

3) Launch VS Code Quick Open (Ctrl+P), paste the following command, and press enter : 
```ext install temilaj.asp-net-core-vs-code-extension-pack```

If error with omnisharp on ubuntu add a symlink : 
https://github.com/OmniSharp/omnisharp-vscode/issues/4201
# TO RUN

dotnet run