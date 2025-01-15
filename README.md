Aplikacja internetowa służąca przeglądaniu oraz wystawianiu ocen i recenzji twoich ulubionych albumów muzycznych. 
Strona dostępna jest pod adresem: https://music4you.azurewebsites.net/

Aby uruchomić projekt lokalnie na swoim komputerze należy:
1. Sklonować repozytorium na swoją maszynę.
2. Uruchomić plik music4you.sln w Visual Studio 2022
3. Zainstalować wymagane rozszerzenia za pomocą narzędzia NuGet
4. Utworzyć własny plik appsettings.json z ConnectionString do własnej bazy SQL Server według załączonego wzorca lub skontaktować się z autorem w celu podłączenia się do bazy autora.
5. W konsoli cmd, w folderze projektu (\music4you\music4you\), wykonać migrację oraz zaaktualizować bazę:
 Add-Migration migracja1
 Update-Database
6. Skompilować i uruchomić projekt w Visual Studio 2022.
7. Aplikacja powinna zostać uruchomiona w przeglądarce.
