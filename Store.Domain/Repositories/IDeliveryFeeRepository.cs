using Store.Domain.Entities;

namespace Store.Domain.Repositories;

public interface IDeliveryFeeRepository
{
    double GetDeliveryFee(string zipCode);
}
