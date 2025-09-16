using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Models;



/// <summary>
/// DISCLAIMER: This is only here to help enable the purpose of this exercise, this doesn't reflect the way we work!
/// </summary>


namespace Greggs.Products.Api.DataAccess
{
    public class ProductAccess : IDataAccess<Product>
    {
        public IEnumerable<Product> GetLatestProducts()
        {
            return new List<Product>
            {
                new Product { Name = "Sausage Roll", PriceInPounds = 1.20m },
                new Product { Name = "Vegan Bake", PriceInPounds = 1.50m },
                new Product { Name = "Steak Bake", PriceInPounds = 1.80m }
            };
        }
    }
}
