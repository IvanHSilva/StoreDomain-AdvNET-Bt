using Store.Domain.Commands;

namespace Store.Domain.Utils;

public static class ExtractGuids
{
    public static IEnumerable<Guid> Extract(IList<CreateOrderItemCommand> items)
    {
        List<Guid> guids = [];
        foreach (CreateOrderItemCommand item in items)
        {
            guids.Add(item.Product);
        }

        return guids;
    }
}
