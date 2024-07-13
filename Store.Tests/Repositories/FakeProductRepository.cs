using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories;

public class FakeProductRepository : IProductRepository
{
    public IEnumerable<Product> GetProducts(IEnumerable<Guid> ids)
    {
        IList<Product> products = [];
        products.Add(new Product("Prod01", 10, true));
        products.Add(new Product("Prod02", 20, true));
        products.Add(new Product("Prod03", 30, true));
        products.Add(new Product("Prod04", 40, false));
        products.Add(new Product("Prod05", 50, false));

        return products;
    }
}
