using Store.Domain.Repositories;

namespace Store.Tests.Repositories;

public class FakeDeliveryFeeRepository : IDeliveryFeeRepository
{
    public double GetDeliveryFee(string zipCode)
    {
        return 10;
    }
}
