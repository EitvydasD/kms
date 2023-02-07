using KMS.Core.Aggregates.User.Entities;
using Utils.Library.Interfaces;

namespace KMS.Core.Aggregates.Trip.Entities;

public class UserTripEntity : IAggregateRoot
{
    public Guid TripId { get; set; }
    
    public TripEntity? Trip { get; set; }
    
    public Guid UserId { get; set; }

    public UserEntity? User { get; set; }
}
