using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities;

[TestClass]
public class OrderTests
{
    private static readonly Customer _customer = new("Ivan Henriques", "ivanhs@gmail.com");
    private readonly Order _order = new(_customer, 0, null!);
    private readonly Product _product = new("Produ1", 10, true);
    private readonly Discount _discount = new(10, DateTime.Now.AddDays(5));

    [TestMethod]
    [TestCategory("Domain")]
    public void NewOrderShouldGenerateNumberWithEightCharacters()
    {
        Assert.AreEqual(8, _order.Number.Length);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void NewOrderShouldGenerateStatusWaitingPayment()
    {
        Assert.AreEqual(_order.OrderStatus, EOrderStatus.WaitingPayment);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void OrderWithPaymentShouldGenerateStatusWaitingDelivery()
    {
        _order.AddItem(_product, 1);
        _order.Pay(10);
        Assert.AreEqual(_order.OrderStatus, EOrderStatus.WaitingDelivery);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void CancelledOrderShouldGenerateStatusCancel()
    {
        _order.Cancel();
        Assert.AreEqual(_order.OrderStatus, EOrderStatus.Canceled);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void NewOrderItemWithoutProductItemShouldNotBeAdded()
    {
        _order.AddItem(_product, 0);
        Assert.AreEqual(_order.Items.Count, 0);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void NewOrderItemWithZeroQuantityShouldNotBeAdded()
    {
        _order.AddItem(_product, 0);
        Assert.AreEqual(_order.Items.Count, 0);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void NewOrderTotalValueShouldBeFifth()
    {
        _order.AddItem(_product, 5);
        Assert.AreEqual(_order.Total(), 50);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ExpriredDiscountTotalOrderShoulBeSixty()
    {
        Discount expiredDiscount = new(10, DateTime.Now.AddDays(-5));
        Order order = new(_customer, 10, expiredDiscount);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 60);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void InvalidDiscountTotalOrderShoulBeSixty()
    {
        Order order = new(_customer, 10, null!);
        order.AddItem(_product, 5);
        Assert.AreEqual(_order.Total(), 60);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ValueOfDiscountIsTenTotalOrderShoulBeFifty()
    {
        Order order = new(_customer, 10, _discount);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 50);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ValueOfDeliveryFeeIsTenTotalOrderShoulBeFifty()
    {
        Order order = new(_customer, 10, _discount);
        order.AddItem(_product, 6);
        Assert.AreEqual(order.Total(), 60);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void OrderWithoutCustomerShouldGenerateInvalidOrder()
    {
        Order order = new(null!, 10, _discount);
        Assert.AreEqual(order.IsValid, false);
    }
}
