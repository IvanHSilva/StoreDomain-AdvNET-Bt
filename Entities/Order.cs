using StoreDomain.Enums;

namespace StoreDomain.Entities;

public class Order(Costumer costumer, double deliveryFee, Discount discount) : Entity
{

    public Costumer Costumer { get; private set; } = costumer;
    public DateTime OrderDate { get; private set; } = DateTime.Now;
    public string Number { get; private set; } = Guid.NewGuid().ToString()[..8];
    public IList<OrderItem> Items { get; private set; } = [];
    public double DeliveryFee { get; private set; } = deliveryFee;
    public Discount Discount { get; private set; } = discount;
    public EOrderStatus OrderStatus { get; private set; } = EOrderStatus.WaitingPayment;

    public void AddItem(Product product, int quantity)
    {
        OrderItem item = new(product, product.Price, quantity);
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
            this.OrderStatus = EOrderStatus.WaitingDelivery;
    }

    public void Cancel(double amount)
    {
        OrderStatus = EOrderStatus.Canceled;
    }
}
