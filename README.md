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
dotnet add package ..\..\..\packages\Danstagram.Inventory.Contracts.1.0.0.nupkg
```

Add reference to classlib:

```powershell
dotnet add reference ..\Danstagram.Inventory.Contracts\Danstagram.Inventory.Contracts.csproj
```

```powershell
dotnet add package Microsoft.Extensions.Configuration
dotnet add package MongoDB.Driver
dotnet add package Microsoft.Extensions.Configuration.Binder
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Http.Polly
dotnet add package MassTransit.AspNetCore
dotnet add package MassTransit.RabbitMQ
dotnet dev-certs https --trust
dotnet nuget add source 'D:\...' -n Danstagram

```
