using Ardalis.SmartEnum;

namespace KMS.Core.Aggregates.Trip;

public class TripStatus : SmartEnum<TripStatus>
{
    public static readonly TripStatus Pending = new TripStatus(nameof(Pending), 10);
    public static readonly TripStatus InProgress = new TripStatus(nameof(InProgress), 20);
    public static readonly TripStatus Delivered = new TripStatus(nameof(Delivered), 30);

    public TripStatus(string name, int value) : base(name, value)
    {
    }

    public static implicit operator int(TripStatus item) => item.Value;

    public static implicit operator TripStatus(int value) => FromValue(value);
}
