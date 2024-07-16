using Flunt.Notifications;
using Flunt.Validations;

namespace Store.Domain.Commands;

public class CreateOrderCommand : Notifiable<Notification>, ICommand
{
    public CreateOrderCommand()
    {
        Items = [];
    }

    public CreateOrderCommand(string customer, string zipCode, string promoCode, IList<CreateOrderItemCommand> items)
    {
        Customer = customer;
        ZipCode = zipCode;
        PromoCode = promoCode;
        Items = items;
    }

    public string Customer { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string PromoCode { get; set; } = string.Empty;
    public IList<CreateOrderItemCommand> Items { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<Notification>().Requires()
        .IsNullOrEmpty(Customer, "Customer", "Cliente inválido!")
        .IsNullOrEmpty(ZipCode, "ZipCode", "CEP inválido!")
        );
    }
}
