using System.Collections.Generic;

namespace Greggs.Products.Api.DataAccess;

public  interface IDataAccess<T>
{
    IEnumerable<T> GetLatestProducts();
}

