# ModularArchitecture

### Technology Stack :muscle:

- API - ASP.NET Core 5.0 WebAPI
- Client - Angular 12 Material
- Data Access - [Entity Framework Core 5.0](https://docs.microsoft.com/en-us/ef/core/)
- DB Providers - MSSQL

#### Prerequisites to run API

1. Install the latest [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
2. Install the latest DOTNET & EF CLI Tools by using this command `dotnet tool install --global dotnet-ef` 
3. Install the latest version of Visual Studio IDE 2019 (v16.8 and above) OR Visual Studio Code

#### Running the API

1. Open up `ModularArchitecture.sln` in Visual Studio 2019.
2. Navigate to appSettings.json under `src/Api/Bootstrapper/appsettings.json`
3. Add you MSSQL connection string under `PersistenceSettings`. The default connection string is `"mssql": "Data Source=.; Initial Catalog=ModularArchitecture; Integrated Security=true; Max Pool Size=1000; Min Pool Size=12; Pooling=True;"`
4. That's everything you need to setup the API. Just build and run the API project.
5. By default, the database is migrated and latest changes are applied.
6. Some default data is also seeded to this database like roles, users, brands, products etc.


#### Default Credentials

- superadmin - admin@admin.com / 123Pa$$word!

You can use these credentials to generate jwt tokens in the `api/identity/tokens` endpoint.
