# Jobsity Code Challenge: Financial Chat

## Technologies

* .NET Core 3.1
* ASP .NET Core 3.1
* Blazor
* Entity Framework Core 5.0.0
* Identity
* SignalR
* MediatR
* RabbitMQ
* AutoMapper
* FluentValidation
* SqlServer
* Migrations
* XUnit, 
* Moq 
* Shouldly

## Setup

To run this solution completely you will need to start two projects:
1. Worker: Navigate to `src/1-Presentation/Jobsity.FinancialChat.StockQuoteBot` and run `dotnet run` to launch the project
2. WebUI: Navigate to `src/1-Presentation/Jobsity.FinancialChat.WebUI` and run `dotnet run` to launch the project
3. Open your browser at `https://localhost:5001`
4. You can create an account or use any of the default users:

```
benjamin.graham@jobsity.com
john.templeton@jobsity.com
john.neff@jobsity.com
jesse.livermore@jobsity.com
peter.lynch@jobsity.com
george.soros@jobsity.com
warren.buffett@jobsity.com
john.bogle@jobsity.com
carl.icahn@jobsity.com
william.gross@jobsity.com

passwords: Jobsity@123
```

### Database Configuration

The solution is configured to use an **SQL Server** database by default.

Verify that the **DefaultConnection** connection string within **src/1-Presentation/Jobsity.FinancialChat.WebUI/appsettings.json** points to a valid SQL Server instance. 

When you run the application, the database will be automatically created (if necessary) and, the latest migrations, will be applied.

>If you just want to run the solution without needing to set up a database infrastructure you can switch to an **In-Memory Database**, you just need to update **src/1-Presentation/Jobsity.FinancialChat.WebUI/appsettings.json** as follows:

```json
  "UseInMemoryDatabase": true,
```

### RabbitMQ Server

By default, the solution will connect to a `localhost` RabbitMQ Server but, you can change these configurations in the `appsettings.json` files located at:

- `src/1-Presentation/Jobsity.FinancialChat.WebUI/appsettings.json`
- `src/1-Presentation/Jobsity.FinancialChat.StockQuoteBot/appsettings.json`

```json
  "RabbitMqHostName": "localhost",
  "RabbitMqStockQuotesQueueName": "stock_quotes_requests",
  "RabbitMqChatRoomQueueName": "chat_room"
```

Please, keep this information equal in both `appsettings.json` files.

## Overview

This solution uses **CQRS** design pattern and **Clean Architecture**, where the different parts of the system have a low degree of dependence, that is, poor coupling, resulting in easy maintainability and testability.

Structure:
```
/Jobsity.FinancialChat.sln 
|--- src  
|	 |--- 1-Presentation  
|	 |	  |--- Jobsity.FinancialChat.StockQuoteBot  
| 	 |	  |--- Jobsity.FinancialChat.WebUI  
|	 |--- 2-Core  
| 	 |	  |--- Jobsity.FinancialChat.Application  
|    |    |--- Jobsity.FinancialChat.Domain  
|    |--- 3-Infrastructure 
|    |    |--- Jobsity.FinancialChat.Infrastructure  
|    |    |--- Jobsity.FinancialChat.Persistence  
|    |    |--- Jobsity.FinancialChat.IoC  
|--- tests
|    |--- Jobsity.FinancialChat.Application.Tests
|    |--- Jobsity.FinancialChat.Infrastructure.Tests
```
