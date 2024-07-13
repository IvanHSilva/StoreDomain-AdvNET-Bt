using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories;

public class FakeCustomerRepository : ICustomerRepository
{
    public Customer GetCustomer(string document)
    {
        if (document == "12345678910")
            return new Customer("José Maria", "josemaria@gmail.com");

        return null!;
    }
}
