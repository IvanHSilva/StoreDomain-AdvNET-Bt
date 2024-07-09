using Flunt.Notifications;
using Flunt.Validations;

namespace StoreDomain.Entities;

public class OrderItem : Entity
{

    public Product Product { get; private set; }
    public double Price { get; private set; }
    public int Quantity { get; private set; }

    public OrderItem(Product product, double price, int quantity)
    {
        AddNotifications(new Contract<Notification>().Requires()
        .IsNotNull(product, "Product", "Produto inválido!")
        .IsGreaterThan(quantity, 0, "Quantity", "Quantidade deve ser maior que zero!"));

        Product = product;
        Price = price;
        Quantity = quantity;
    }

    public double Total()
    {
        return Price * Quantity;
    }
}
