using System.Net.NetworkInformation;

namespace StoreDomain.Entities;

public class Product(string title, double price, bool active) : Entity
{
    public string Title { get; private set; } = title;
    public double Price { get; private set; } = price;
    public bool Active { get; private set; } = active;

    public void ChangeInfo(string title, double price, bool active)
    {
        Title = title;
        Price = price;
        Active = active;
    }
}
