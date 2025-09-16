using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    
    private const decimal ExchangeRate = 1.11m;

    private readonly IDataAccess<Product> _dataAccess;

    public ProductController(ILogger<ProductController> logger, IDataAccess<Product> dataAccess)
    {
        _logger = logger;
        _dataAccess = dataAccess;
    }


    // User Story 1: Get latest products from data access
   
    [HttpGet("latest")]
    public IEnumerable<Product> GetLatest(int pageStart = 0, int pageSize = 5)
    {
        _logger.LogInformation("Fetching latest products: pageStart={PageStart}, pageSize={PageSize}", pageStart, pageSize);

        var products = _dataAccess.GetLatestProducts()
                                  .Skip(pageStart)
                                  .Take(pageSize)
                                  .ToList();

        _logger.LogInformation("Returned {Count} products", products.Count);

        return products;
    }

    // User Story 2: Get products with prices in Euros
    [HttpGet("euro")]
    public IEnumerable<ProductEuroDto> GetProductsInEuro(int pageStart = 0, int pageSize = 5)
    {
        _logger.LogInformation("Fetching products in Euro: pageStart={PageStart}, pageSize={PageSize}", pageStart, pageSize);

        var products = _dataAccess.GetLatestProducts()
                                  .Skip(pageStart)
                                  .Take(pageSize)
                                  .ToList();

        var euroProducts = products.Select(p => new ProductEuroDto
        {
            Name = p.Name,
            PriceInPounds = p.PriceInPounds,
            PriceInEuros = Math.Round(p.PriceInPounds * ExchangeRate, 2)
        }).ToList();

        _logger.LogInformation("Converted {Count} products to Euro pricing", euroProducts.Count);

        return euroProducts;
    }

}
