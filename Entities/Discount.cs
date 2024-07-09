namespace StoreDomain.Entities;

public class Discount(double amount, DateTime expireDate) : Entity
{

    public double Amount { get; private set; } = amount;
    public DateTime ExpireDate { get; private set; } = expireDate;

    public bool IsValid()
    {
        return DateTime.Compare(DateTime.Now, ExpireDate) < 0;
    }

    public double Value()
    {
        if (IsValid())
            return Amount;
        else
            return 0;
    }
}
