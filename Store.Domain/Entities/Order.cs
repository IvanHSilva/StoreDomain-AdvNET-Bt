using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Enums;

namespace Store.Domain.Entities;

public class Order : Entity
{
    public Customer Customer { get; private set; }
    public DateTime OrderDate { get; private set; }
    public string Number { get; private set; } = string.Empty;
    public IList<OrderItem> Items { get; private set; }
    public double DeliveryFee { get; private set; }
    public Discount Discount { get; private set; }
    public EOrderStatus OrderStatus { get; private set; }

    public Order(Customer customer, double deliveryFee, Discount discount)
    {
        AddNotifications(new Contract<Notification>().Requires().IsNotNull(customer,
        "Customer", "Cliente inválido!"));

        Customer = customer;
        OrderDate = DateTime.Now;
        Number = Guid.NewGuid().ToString()[..8];
        OrderStatus = EOrderStatus.WaitingPayment;
        DeliveryFee = deliveryFee;
        Discount = discount;
        Items = [];
    }

    public void AddItem(Product product, int quantity)
    {
        OrderItem item = new(product, product.Price, quantity);
        if (item.IsValid)
            Items.Add(item);
    }

    public double Total()
    {
        double total = 0;
        foreach (OrderItem item in Items)
        {
            total += item.Total();
        }

        total += DeliveryFee;
        total -= Discount != null ? Discount.Value() : 0;

        return total;
    }

    public void Pay(double amount)
    {
        if (amount == Total())
            OrderStatus = EOrderStatus.WaitingDelivery;
    }

    public void Cancel()
    {
        OrderStatus = EOrderStatus.Canceled;
    }
}
