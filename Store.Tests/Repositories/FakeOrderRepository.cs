using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories;

public class FakeOrderRepository : IOrderRepository
{
    public void SaveOrder(Order order) { }
}
