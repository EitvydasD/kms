using KMS.Core.Aggregates.Trip.Requests;
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

    public static TripEntity Create(Guid callerId, CreateTripRequest request)
    {
        var tripId = Guid.NewGuid();

        var trip = new TripEntity
        {
            Id = tripId,
            DriverId = request.DriverId ?? callerId,
            DepartedAt = request.DepartedAt,
            ArrivedAt = request.ArrivedAt,
            Status = request.Status,
            Responsible = new List<UserTripEntity>
            {
                new UserTripEntity{ UserId = callerId, TripId = tripId }
            },
        };

        return trip;
    }

    public void Update(TripEntity request)
    {
        DriverId = request.DriverId;
        DepartedAt = request.DepartedAt;
        ArrivedAt = request.ArrivedAt;
        Status = request.Status;
        Responsible = request.Responsible;
    }
}