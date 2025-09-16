# Greggs.Products API

Welcome to the Greggs Products API! This project provides endpoints to retrieve the latest Greggs menu items and convert their prices to Euros. It demonstrates clean architecture, structured logging, and AutoMapper integration.

---

## Features

- Retrieve latest products using a data access layer
-  Convert product prices from GBP to EUR
-  Logging with `ILogger` for traceability
-  AutoMapper for DTO mapping
-  Unit tested with xUnit and Moq
-  Clean separation of concerns (Controller → Service → DataAccess)

---

## Project Structure

Greggs.Products/ ├── Api/ │ ├── Controllers/ │ │ └── ProductController.cs │ ├── Models/ │ │ ├── Product.cs │ │ └── ProductEuroDto.cs │ ├── Services/ │ │ └── ProductService.cs │ ├── Mapping/ │ │ └── MappingProfile.cs ├── UnitTests/ │ └── ProductServiceTests.cs

Code

---

## User Stories

### User Story 1: Latest Products

**As a** Greggs Fanatic  
**I want to** get the latest menu of products  
**So that** I see the most recently available items

✅ Implemented via `GET /product/latest`  
Uses `_dataAccess.GetLatestProducts()` with paging support.

---

### User Story 2: Prices in Euros

**As a** Greggs Entrepreneur  
**I want to** get product prices in Euros  
**So that** I can plan European expansion

Implemented via `GET /product/euro`  
Uses AutoMapper to convert prices with a fixed exchange rate of `1.11`.

---

## 🔧 Endpoints

### `GET /product/latest`

Returns a paginated list of products from the data access layer.

```csharp
public IEnumerable<Product> GetLatest(int pageStart = 0, int pageSize = 5)
GET /product/euro
Returns a paginated list of products with prices converted to Euros.

csharp
public IEnumerable<ProductEuroDto> GetProductsInEuro(int pageStart = 0, int pageSize = 5)
AutoMapper Setup
MappingProfile.cs
csharp
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductEuroDto>()
            .ForMember(dest => dest.PriceInEuros,
                       opt => opt.MapFrom(src => Math.Round(src.PriceInPounds * 1.11m, 2)));
    }
}
Registration
csharp
services.AddAutoMapper(typeof(MappingProfile));
Unit Testing
Tests written using xUnit and Moq. Example:

csharp
[Fact]
public void GetProductsInEuro_MapsCorrectly()
{
    var mockDataAccess = new Mock<IDataAccess<Product>>();
    mockDataAccess.Setup(d => d.GetLatestProducts()).Returns(new List<Product>
    {
        new Product { Name = "Sausage Roll", PriceInPounds = 1.00m },
        new Product { Name = "Vegan Bake", PriceInPounds = 2.00m }
    });

    var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
    var mapper = config.CreateMapper();

    var service = new ProductService(mockDataAccess.Object, mapper);

    var result = service.GetProductsInEuro(0, 2).ToList();

    Assert.Equal(1.11m, result[0].PriceInEuros);
    Assert.Equal(2.22m, result[1].PriceInEuros);
}
 How to Run
Clone the repo

Restore packages:

bash
dotnet restore
Build the solution:

bash
dotnet build
Run tests:

bash
dotnet test
Notes
Exchange rate is hardcoded as 1.11m for GBP → EUR conversion.

Paging is handled via pageStart and pageSize query parameters.

Logging is added for both endpoints using ILogger<ProductController>.
