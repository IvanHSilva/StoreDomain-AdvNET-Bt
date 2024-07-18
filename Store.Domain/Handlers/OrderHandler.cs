using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands;
using Store.Domain.Commands.Intefaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Intefaces;
using Store.Domain.Repositories;
using Store.Domain.Utils;

namespace Store.Domain.Handlers;

public class OrderHandler : Notifiable<Notification>, IHandler<CreateOrderCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IDeliveryFeeRepository _deliveryFeeRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderHandler(ICustomerRepository customerRepository, IDeliveryFeeRepository deliveryFeeRepository,
    IDiscountRepository discountRepository, IProductRepository productRepository, IOrderRepository orderRepository)
    {
        _customerRepository = customerRepository;
        _deliveryFeeRepository = deliveryFeeRepository;
        _discountRepository = discountRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }


    // public CreateOrderCommand()
    // {
    //     Items = [];
    // }

    // public CreateOrderCommand(string customer, string zipCode, string promoCode, IList<CreateOrderItemCommand> items)
    // {
    //     Customer = customer;
    //     ZipCode = zipCode;
    //     PromoCode = promoCode;
    //     Items = items;
    // }

    // public string Customer { get; set; } = string.Empty;
    // public string ZipCode { get; set; } = string.Empty;
    // public string PromoCode { get; set; } = string.Empty;
    // public IList<CreateOrderItemCommand> Items { get; set; }

    // public void Validate()
    // {
    //     AddNotifications(new Contract<Notification>().Requires()
    //     .IsNullOrEmpty(Customer, "Customer", "Cliente inválido!")
    //     .IsNullOrEmpty(ZipCode, "ZipCode", "CEP inválido!")
    //     );
    // }
    public ICommandResult Handle(CreateOrderCommand command)
    {
        command.Validate();
        if (!command.IsValid)
            return new GenericCommandResult(false, "Pedido inválido!", command.Notifications!);

        Customer customer = _customerRepository.GetCustomer(command.Customer);
        double deliveryFee = _deliveryFeeRepository.GetDeliveryFee(command.ZipCode);
        Discount discount = _discountRepository.GetDiscount(command.PromoCode);

        List<Product> products = _productRepository.GetProducts(ExtractGuids.Extract(command.Items)).ToList();
        Order order = new(customer, deliveryFee, discount);
        foreach (CreateOrderItemCommand item in command.Items)
        {
            Product product = products.Where(p => p.Id == item.Product).First();
            order.AddItem(product, item.Quantity);
        }

        AddNotifications(order.Notifications);

        if (!IsValid)
            return new GenericCommandResult(false, "Falha ao gerar o Pedido!", Notifications);

        _orderRepository.SaveOrder(order);
        return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso!", order);
    }
}
