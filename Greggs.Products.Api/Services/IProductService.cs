using System.Collections.Generic;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetLatestProducts(int pageStart, int pageSize);
        IEnumerable<ProductEuroDto> GetProductsInEuro(int pageStart, int pageSize);
    }

}
