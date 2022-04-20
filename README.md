# AuctionApp - Lot Module!

AuctionApp - Lot Module is a web based solution where brands can conduct online auctions and manage their lot management engine. It has functionality to Create, edit, delete and get lot. Lot has details like AuctionId, LotId, Opening price, Reserve price, quantity and Increment table with different price range.

## Getting started


### Prerequisites  

 - Visual Studio 2019 or later  
 - .Net core (3.1)
 - Azure cosmos DB explorer


### Dependancies
#### Nuget Packages
    - AutoMapper.Extensions.Microsoft.DependencyInjection
    - Microsoft.Azure.Cosmos
    - Microsoft.Extensions.DependencyInjection.Abstractions
    - Microsoft.Extensions.Http.Polly
    - Microsoft.Extensions.Options
    - Microsoft.NET.Test.Sdk
    - Moq
    - RestSharp
    - Swashbuckle.AspNetCore
    - System.Net.Http.Json
    - xunit
    - xunit.runner.visualstudio


### Database 
Database Server: Cosmos DB explorer
Database Name: dev-sbs
Database Container: corebidding

### Project Architecture
Specify your project architecture here. Suppose you are following Repository pattern then mention your all the projects along with short description here.

 - Demo.MedTech.Api (End points to manage lot)
 - Demo.MedTech.Common (Contains references of other assemblies that are common among multiple projects) 
 - Demo.MedTech.DAL (Contains database operations)
 - Demo.MedTech.DataModel (Contains request, response and shared data model classes)
 - Demo.MedTech.Service (Contains business logic)
 - Demo.MedTech.Utility (Contains helper methods for business operations)
 - Demo.MedTech.ValidationEngine (Contains logic to validation lot request model)
 - Playground (Web app to interact) 
 - Demo.MedTech.Api.UnitTests (Contains all the test methods)

## Running the tests
In this project we have used XUnit. You can run all the tests from the Test Explorer. If Test Explorer is not visible, choose  **Test**  on the Visual Studio menu, choose  **Windows**, and then choose  **Test Explorer**. All the unit tests will be listed so choose the test you want to run. You can also run alto tests by selecteing "Run All" option.

## Deployment
### Deployement server
If you have multiple server for the stage and production then specify all the details here

### Deployement prerequistes
If you have EC2 instance or any VPS server then install below prerequistes software in the machine
1. Install IIS
2. Install .Net Framework
### Deployement Steps 
1. Create virtual directory under IIS
2. Enable Inbound and Outbound port for SQL Server(1433) in Firewall(If required)
3. Create security group in your production and stage server if you are using EC2 
4. Update the Database Connection string with Production in web.config
5. Publish your project         
6. Move your publish files to the stage/production server

## License
This project is licensed under the Simform Solutions Pvt. Ltd




