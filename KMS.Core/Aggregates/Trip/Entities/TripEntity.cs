using KMS.Core.Aggregates.User.Entities;
using Utils.Library.Interfaces;

namespace KMS.Core.Aggregates.Trip.Entities;

public class TripEntity : BaseEntity, IAggregateRoot
{
    public Guid DriverId { get; set; }

    public UserEntity? Driver { get; set; }

    public DateTime? DepartedAt { get; set; }

    public DateTime? ArrivedAt { get; set; }

    public List<UserTripEntity> Responsible { get; set; } = new();

    public TripStatus Status { get; set; } = TripStatus.Pending;
}
