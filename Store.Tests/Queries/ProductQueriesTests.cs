using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Queries;

namespace Store.Tests.Queries
{
    [TestClass]
    public class ProductQueriesTests
    {
        private readonly IList<Product> _products;

        public ProductQueriesTests()
        {
            _products = [];
            _products.Add(new Product("Prod01", 10, true));
            _products.Add(new Product("Prod02", 20, true));
            _products.Add(new Product("Prod03", 30, true));
            _products.Add(new Product("Prod04", 40, false));
            _products.Add(new Product("Prod05", 50, false));
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void ActiveProductsQuerieShouldReturn3()
        {
            IQueryable<Product> result = _products.AsQueryable()
            .Where(ProductQueries.GetActiveProducts());
            Assert.AreEqual(result.Count(), 3);
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void InactiveProductsQuerieShouldReturn2()
        {
            IQueryable<Product> result = _products.AsQueryable()
            .Where(ProductQueries.GetInactiveProducts());
            Assert.AreEqual(result.Count(), 2);
        }
    }
}
