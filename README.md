# NordicStore

A bootstrap project, in dotnet core, to show some architectural patterns really useful when building APIs with heavy business rules.


## Architecture Patterns

- Domain Driven Design
- Anti-Corruption Layer
- CQRS
- Interface Segragation in repositories
- Notification Pattern


## 3rd Party Tools  
- [Dapper](https://github.com/StackExchange/Dapper) for data access
- [Swagger](https://swagger.io/) for documentation
- [Azure Webapp](https://azure.microsoft.com/en-us/services/app-service/web/) to publish

## Cosidatations: 
- The unit tests were focused on Entities, Value Objects and Handlers. Where the business rules must me concentrated