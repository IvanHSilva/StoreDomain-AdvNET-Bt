using Store.Domain.Commands.Intefaces;
using Store.Domain.Handlers;
using Store.Domain.Repositories;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers;

[TestClass]
public class OrderHandlerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IDeliveryFeeRepository _deliveryFeeRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderHandlerTests()
    {
        _customerRepository = new FakeCustomerRepository();
        _deliveryFeeRepository = new FakeDeliveryFeeRepository();
        _discountRepository = new FakeDiscountRepository();
        _orderRepository = new FakeOrderRepository();
        _productRepository = new FakeProductRepository();
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void NonexistentCustomerShouldNotGeneratOrder()

    {
        // TODO: Implementar
        Assert.IsTrue(true);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void InvalidZipCodeShouldGeneratOrder()
    {
        // TODO: Implementar
        Assert.IsTrue(true);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void NonexistentPromoCodeShouldGeneratOrder()
    {
        // TODO: Implementar
        Assert.IsTrue(true);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void NonexistentOderItemsShouldNotGeneratOrder()
    {
        // TODO: Implementar
        Assert.IsTrue(true);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void InvalidCommandShouldNotGeneratOrder()
    {
        CreateOrderCommand command = new()
        {
            Customer = "",
            ZipCode = "13411080",
            PromoCode = "12345678"
        };
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Validate();

        Assert.AreEqual(command.IsValid, false);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ValidCommandShouldGeneratOrder()
    {
        CreateOrderCommand command = new()
        {
            Customer = "12345678",
            ZipCode = "13411080",
            PromoCode = "12345678"
        };
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        OrderHandler handler = new(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _productRepository,
            _orderRepository);

        handler.Handle(command);
        Assert.AreEqual(handler.IsValid, true);
    }
}
