using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Mapping;
using Moq;
using AutoMapper;

namespace Greggs.Products.UnitTests
{
    public class ProductServiceTests
    {
        private readonly IMapper _mapper;

        public ProductServiceTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void GetProductsInEuro_ReturnsConvertedPrices()
        {
            var mockDataAccess = new Mock<IDataAccess<Product>>();
            mockDataAccess.Setup(d => d.GetLatestProducts()).Returns(new List<Product>
            {
                new Product { Name = "Sausage Roll", PriceInPounds = 1.00m },
                new Product { Name = "Vegan Bake", PriceInPounds = 2.00m }
            });

            var service = new ProductService(mockDataAccess.Object, _mapper);

            var result = service.GetProductsInEuro(0, 2).ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal(1.11m, result[0].PriceInEuros);
            Assert.Equal(2.22m, result[1].PriceInEuros);
        }

        [Fact]
        public void GetLatestProducts_ReturnsCorrectPage()
        {
            var mockDataAccess = new Mock<IDataAccess<Product>>();
            mockDataAccess.Setup(d => d.GetLatestProducts()).Returns(Enumerable.Range(1, 10).Select(i =>
                new Product { Name = $"Product{i}", PriceInPounds = i }));

            var service = new ProductService(mockDataAccess.Object, _mapper);

            var result = service.GetLatestProducts(2, 3).ToList();

            Assert.Equal(3, result.Count);
            Assert.Equal("Product3", result[0].Name);
            Assert.Equal("Product5", result[2].Name);
        }

        [Fact]
        public void GetProductsInEuro_HandlesEmptyList()
        {
            var mockDataAccess = new Mock<IDataAccess<Product>>();
            mockDataAccess.Setup(d => d.GetLatestProducts()).Returns(new List<Product>());

            var service = new ProductService(mockDataAccess.Object, _mapper);

            var result = service.GetProductsInEuro(0, 5);

            Assert.Empty(result);
        }

        [Fact]
        public void GetProductsInEuro_MapsCorrectly()
        {
            var mockDataAccess = new Mock<IDataAccess<Product>>();
            mockDataAccess.Setup(d => d.GetLatestProducts()).Returns(new List<Product>
            {
                new Product { Name = "Sausage Roll", PriceInPounds = 1.00m },
                new Product { Name = "Vegan Bake", PriceInPounds = 2.00m }
            });

            var service = new ProductService(mockDataAccess.Object, _mapper);

            var result = service.GetProductsInEuro(0, 2).ToList();

            Assert.Equal(1.11m, result[0].PriceInEuros);
            Assert.Equal(2.22m, result[1].PriceInEuros);
        }
    }
}
