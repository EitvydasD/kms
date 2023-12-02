using KMS.Core.Aggregates.Trip;
using KMS.Core.Aggregates.Trip.Entities;
using KMS.Core.Aggregates.Trip.Requests;
using KMS.Core.Aggregates.Trip.Specs;
using KMS.Core.Exceptions;
using KMS.Core.Interfaces.Trip;
using Microsoft.AspNetCore.SignalR;
using Utils.Library.Interfaces;

namespace KMS.Core.Services.Trip;

public class TripService : ITripService
{
    public TripService(IRepository<TripEntity> tripRepo)
    {
        TripRepo = tripRepo ?? throw new ArgumentNullException(nameof(tripRepo));
    }

    private IRepository<TripEntity> TripRepo { get; }

//GetTrips
    public async Task<ICollection<TripEntity>> GetTrips(GetTripRequest request, Guid callerId, bool hasPermissionViewAll, CancellationToken cancellationToken = default)
    {
        var spec = new GetTripsSpec(request, callerId, hasPermissionViewAll);
        return await TripRepo.ListAsync(spec, cancellationToken);
    }
//GetTrip
    public async Task<TripEntity> GetTrip(Guid tripId, CancellationToken cancellationToken = default)
    {
        var spec = new GetTripSpec(tripId);
        var trip = await TripRepo.FirstOrDefaultAsync(spec, cancellationToken);

        if (trip is null)
        {
            throw new TripNotFoundException(tripId);
        }

        return trip;
    }
//CreateTrip
    public async Task<TripEntity> CreateTrip(CreateTripRequest request, Guid callerId, CancellationToken cancellationToken = default)
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


        await TripRepo.AddAsync(trip, cancellationToken);

        return await GetTrip(trip.Id, cancellationToken);
    }
//UpdateTrip
    public async Task<TripEntity> UpdateTrip(Guid tripId, TripEntity request, CancellationToken cancellationToken = default)
    {
        var trip = await GetTrip(tripId, cancellationToken);

        trip.Update(request);

        await TripRepo.SaveChangesAsync(cancellationToken);
        
        return trip;
    }
//DeleteTrip
    public async Task DeleteTrip(Guid tripId, CancellationToken cancellationToken = default)
    {
        var trip = await GetTrip(tripId, cancellationToken);

        if (trip.Status != TripStatus.Pending)
        {
            throw new TripCannotBeDeletedException(tripId);
        }

        await TripRepo.DeleteAsync(trip, cancellationToken);
    }
}
