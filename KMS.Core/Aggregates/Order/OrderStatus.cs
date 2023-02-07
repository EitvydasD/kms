using Ardalis.SmartEnum;

namespace KMS.Core.Aggregates.Order;
public class OrderStatus : SmartEnum<OrderStatus>
{
    public static readonly OrderStatus Pending = new(nameof(Pending), 1);
    public static readonly OrderStatus Completed = new(nameof(Completed), 2);
    public static readonly OrderStatus Cancelled = new(nameof(Cancelled), 99);

    private OrderStatus(string name, int value) : base(name, value)
    {
    }

    public static implicit operator int(OrderStatus item) => item.Value;

    public static implicit operator OrderStatus(int value) => FromValue(value);
}
