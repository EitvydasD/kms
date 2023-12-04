using KMS.Core.Aggregates.Trip.Entities;
using KMS.Core.Aggregates.Trip.Requests;

namespace KMS.Core.Interfaces.Trip;

public interface ITripService
{
    Task<ICollection<TripEntity>> GetTrips(GetTripRequest request, Guid callerId, bool hasPermissionViewAll, CancellationToken cancellationToken = default);

    Task<TripEntity> GetTrip(Guid tripId, CancellationToken cancellationToken = default);
    Task<TripEntity> GetTripById(Guid tripId, CancellationToken cancellationToken = default);
    
    Task<TripEntity> CreateTrip(CreateTripRequest request, Guid callerId, CancellationToken cancellationToken = default);
    
    Task<TripEntity> UpdateTrip(Guid tripId, TripEntity request, CancellationToken cancellationToken = default);

    Task DeleteTrip(Guid tripId, CancellationToken cancellationToken = default);
}
