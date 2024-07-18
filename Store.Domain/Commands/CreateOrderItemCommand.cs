using Flunt.Notifications;
using Flunt.Validations;

namespace Store.Domain.Commands.Intefaces;

public class CreateOrderItemCommand : Notifiable<Notification>, ICommand
{
    public CreateOrderItemCommand() { }

    public CreateOrderItemCommand(Guid product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public Guid Product { get; set; }
    public int Quantity { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<Notification>().Requires()
        .IsNullOrEmpty(Product.ToString(), "Product", "Produto inválido!")
        .IsGreaterThan(Quantity, 8, "Quantity", "Quantidade inválida!")
        );
    }
}
