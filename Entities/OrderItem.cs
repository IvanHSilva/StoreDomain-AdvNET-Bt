namespace StoreDomain.Entities;

public class OrderItem(Product product, double price, int quantity) : Entity
{

    public Product Product { get; private set; } = product;
    public double Price { get; private set; } = price;
    public int Quantity { get; private set; } = quantity;

    public double Total()
    {
        return Price * Quantity;
    }
}
