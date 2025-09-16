Greggs.Products API
===================

Welcome to the Greggs Products API! This project provides endpoints to retrieve the latest Greggs menu items and convert their prices to Euros. It demonstrates clean architecture, structured logging, and AutoMapper integration.
Lead Software Developer Coding Task Submission
Features
--------
- Retrieve latest products using a data access layer
- Convert product prices from GBP to EUR
- Logging with ILogger for traceability
- AutoMapper for DTO mapping
- Unit tested with xUnit and Moq
- Clean separation of concerns (Controller → Service → DataAccess)

Project Structure
-----------------
Greggs.Products/
├── Api/
│   ├── Controllers/
│   │   └── ProductController.cs
│   ├── Models/
│   │   ├── Product.cs
│   │   └── ProductEuroDto.cs
│   ├── Services/
│   │   └── ProductService.cs
│   ├── Mapping/
│   │   └── MappingProfile.cs
├── UnitTests/
│   └── ProductServiceTests.cs

User Stories
------------
User Story 1: Latest Products
As a Greggs Fanatic
I want to get the latest menu of products
So that I see the most recently available items

Implemented via GET /product/latest
Uses _dataAccess.GetLatestProducts() with paging support.

User Story 2: Prices in Euros
As a Greggs Entrepreneur
I want to get product prices in Euros
So that I can plan European expansion

Implemented via GET /product/euro
Uses AutoMapper to convert prices with a fixed exchange rate of 1.11.

Endpoints
---------
GET /product/latest
Returns a paginated list of products from the data access layer.

GET /product/euro
Returns a paginated list of products with prices converted to Euros.

AutoMapper Setup
----------------
MappingProfile.cs:
Maps Product to ProductEuroDto and applies exchange rate conversion.

Registration:
services.AddAutoMapper(typeof(MappingProfile));

Unit Testing
------------
Tests written using xUnit and Moq. Example:
- Verifies Euro conversion logic
- Mocks IDataAccess<Product>
- Uses MapperConfiguration with MappingProfile

How to Run
----------
1. Clone the repo
2. Restore packages: dotnet restore
3. Build the solution: dotnet build
4. Run tests: dotnet test

Notes
-----
- Exchange rate is hardcoded as 1.11m for GBP → EUR conversion.
- Paging is handled via pageStart and pageSize query parameters.
- Logging is added for both endpoints using ILogger<ProductController>.


