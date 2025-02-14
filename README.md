Aplikacja internetowa służąca przeglądaniu oraz wystawianiu ocen i recenzji twoich ulubionych albumów muzycznych. 
Strona dostępna jest pod adresem: https://music4you.azurewebsites.net/

Bogatsza dokumentacja projektu: https://docs.google.com/document/d/16RkSi1zB84AfNM2-0wlRsoAdYCQMBki1cisgCEmJGNo/edit?tab=t.0

Aby uruchomić projekt lokalnie na swoim komputerze należy:
1. Sklonować repozytorium na swoją maszynę.
2. Uruchomić plik music4you.sln w Visual Studio 2022
3. Zainstalować wymagane rozszerzenia za pomocą narzędzia NuGet
   - Microsoft.AspNetCore.Identity.EntityFrameworkCore (8.0.11)
   - Microsoft.EntityFrameworkCore (9.0.0)
   - Microsoft.EntityFrameworkCore.SqlServer (9.0.0)
   - Microsoft.EntityFrameworkCore.Tools (9.0.0)
4. W folderze projektu (\music4you\music4you\) utworzyć własny plik appsettings.json z ConnectionString do własnej bazy SQL Server według załączonego wzorca lub skontaktować się z autorem w celu podłączenia się do bazy autora.

```
{
  "ConnectionStrings": {
    "DefaultConnection": "twoj_connection_string"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

5. W konsoli cmd lub PowerShell, w folderze projektu (\music4you\music4you\), wykonać migrację oraz zaaktualizować bazę:
```
 Add-Migration migracja1
 Update-Database
```
6. Skompilować i uruchomić projekt w Visual Studio 2022.
7. Aplikacja powinna zostać uruchomiona w przeglądarce.
