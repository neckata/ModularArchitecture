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

#### Project Structure

- API / Host – A very thin Rest API / Host Application that is responsible for registering the controllers/services of other modules into the service container.
- Modules – A logical block of the business unit. For example, Slack. Everything that is related to Slack can be found here. We will walk through the definition of a module in the next section.
- Shared Infrastructure – Application-Specific Interfaces and implementations are found here for other modules to consume. This includes Middlewares, Data Access providers, and so on.
- Database

![Project Strucutre](https://raw.githubusercontent.com/neckata/ModularArchitecture/master/About/structure.PNG)

#### Definition of a Module
 - A module is a logical unit of the business requirement. Slack and Outlook are a few examples of Modules.
 - One module should never depend on any other module. It can depend on Abstraction Interfaces that are present in Shared Application Projects.
 - Each module has to follow a domain-driven architecture
 - Every module will be further split into API, Core, and Infrastructure projects to enforce Clean Onion Architecture.
 - Cross Module communication can happen only via Interfaces/events/in-memory bus.

 - Modules.Slack – Contains the API Controllers needed for the module.
 - Modules.Slack.Core – Contains Entities, Abstractions, and everything needed for the module to function independently.
 - Modules.Slack.Infrastructure – This project depends on the Core for abstractions.


