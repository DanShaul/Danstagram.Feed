Create new service:

```powershell
dotnet new webapi -n Danstagram.Inventory.Service
```

Pack to nuget:

```powershell
dotnet pack -p:PackageVersion=1.0.1 -o:../../../packages
```

Create class lib:

```powershell
dotnet new classlib -n Danstagram.Feed.Contracts
```

Add nuget Package:

```powershell
git add package ..\..\..\packages\Danstagram.Inventory.Contracts.1.0.0.nupkg
```

Add reference to classlib:

```powershell
dotnet add reference ..\Danstagram.Inventory.Contracts\Danstagram.Inventory.Contracts.csproj
```
