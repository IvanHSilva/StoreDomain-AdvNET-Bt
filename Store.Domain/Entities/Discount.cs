namespace Store.Domain.Entities;

public class Discount(double amount, DateTime expireDate) : Entity
{

    public double Amount { get; private set; } = amount;
    public DateTime ExpireDate { get; private set; } = expireDate;

    public bool Validate()
    {
        return DateTime.Compare(DateTime.Now, ExpireDate) < 0;
    }

    public double Value()
    {
        if (Validate())
            return Amount;
        else
            return 0;
    }
}
