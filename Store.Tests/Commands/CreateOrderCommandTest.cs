using Store.Domain.Commands;
using Store.Domain.Commands.Intefaces;

namespace Store.Tests.Commands;

[TestClass]
public class CreateOrderCommandTests
{
    [TestMethod]
    [TestCategory("Commands")]
    public void InvalidCommandOrderShoulNotBeGenerated()
    {
        CreateOrderCommand command = new()
        {
            Customer = "",
            ZipCode = "12345678",
            PromoCode = "12345678"
        };
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Validate();

        Assert.AreEqual(command.IsValid, false);
    }
}
