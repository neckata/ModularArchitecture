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
2. Navigate to appSettings.json under `src/Host/ModularArchitecture/appsettings.json`
3. Add you MSSQL connection string under `PersistenceSettings`. The default connection string is `"mssql": "Data Source=.; Initial Catalog=ModularArchitecture; Integrated Security=true; Max Pool Size=1000; Min Pool Size=12; Pooling=True;"`
4. That's everything you need to setup the API. Just build and run the API project.
5. By default, the database is migrated and latest changes are applied.
6. Some default data is also seeded to this database like roles, users, brands, products etc.

#### Default Credentials

- admin - admin@admin.com / 123Pa$$word!

You can use these credentials to generate jwt tokens in the `api/identity/tokens` endpoint.

### Project Structure

- API / Host – A very thin Rest API / Host Application that is responsible for registering the controllers/services of other modules into the service container.
- Modules – A logical block of the business unit. For example, Slack. Everything that is related to Slack can be found here. We will walk through the definition of a module in the next section.
- Shared Infrastructure – Application-Specific Interfaces and implementations are found here for other modules to consume. This includes Middlewares, Data Access providers, and so on.
- Database

![Project Strucutre](https://raw.githubusercontent.com/neckata/ModularArchitecture/master/About/structure.PNG)

 - API will hold all the service / controller registration logics and nothing else.
 - Module.Slack.Core & Module.Outlook.Core will contain the Entity models, interfaces specific to the module and so on.
 - Module.Slack.Infrastructure & Module.Outlook.Infrastructure will mainly hold the Service implementation and the API controllers only, which will be picked up by the API Project
 - Shared.Core will have Common Service Implementations / Interfaces and basically everything that has to be shared across the application.
 - Shared.Models is where the Request /Response classes are added.
 - Shared.Infrastructure contians middlewares, utilities and specify which Database Provider to use for the entire application.

#### Definition of a Module
 - A module is a logical unit of the business requirement. Slack and Outlook are a few examples of Modules.
 - One module should never depend on any other module. It can depend on Abstraction Interfaces that are present in Shared Application Projects.
 - Each module has to follow a domain-driven architecture
 - Every module will be further split into API, Core, and Infrastructure projects to enforce Clean Architecture.
 - Cross Module communication can happen only via Interfaces/events/in-memory bus.

 - Modules.Slack.Core – Contains Entities, Abstractions, and everything needed for the module to function independently.
 - Modules.Slack.Infrastructure – This project depends on the Core for abstractions.

![Modules](https://raw.githubusercontent.com/neckata/ModularArchitecture/master/About/modules.PNG)

### Dependencies 
 - Module.Slack.Core should have a referece to Shared.Core
 - Module.Slack.Infrastructure should have a referece to Shared.Infrastructure & Module.Slack.Core
 - Shared.Infrastructure should have a reference to Shared.Core
 - Shared.Core should depend on Shared.Models

![Dependencies](https://raw.githubusercontent.com/neckata/ModularArchitecture/master/About/dependencies.PNG)

### IConnectorClient 

https://github.com/neckata/ModularArchitecture/blob/master/Shared/Shared.Core/Interfaces/Services/Connector/IConnectorClient.cs

 - UpdateActionAsync
 - CreateActionAsync
 - GetActions

### Swagger
 - There is swagger instaled
 - Module seperation is seen -> Outlook/Slack and their specific endpoints 
 - The "Action" controller is where the diffrent implemantation of the IConnectorClient from the modules comes into use
 - You can add/update/get an action by chosing the specific connector
 - You can get all Connectors from Action controller
 
![Dependencies](https://raw.githubusercontent.com/neckata/ModularArchitecture/master/About/swagger.PNG)
