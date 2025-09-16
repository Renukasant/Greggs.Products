using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IDataAccess<Product> _dataAccess;
        private const decimal ExchangeRate = 1.11m;
        private readonly IMapper _mapper;

        public ProductService(IDataAccess<Product> dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }


        public IEnumerable<Product> GetLatestProducts(int pageStart, int pageSize)
        {
            return _dataAccess.GetLatestProducts()
                              .Skip(pageStart)
                              .Take(pageSize);
        }

        
        public IEnumerable<ProductEuroDto> GetProductsInEuro(int pageStart, int pageSize)
        {
            var products = _dataAccess.GetLatestProducts()
                                      .Skip(pageStart)
                                      .Take(pageSize);

            return _mapper.Map<IEnumerable<ProductEuroDto>>(products);
        }

    }
}