using KMS.API.Models.Trip;
using KMS.Core.Aggregates.Role;
using KMS.Core.Aggregates.Trip.Requests;
using KMS.Core.Attributes;
using KMS.Core.Interfaces.Trip;
using Microsoft.AspNetCore.Mvc;

namespace KMS.API.Controllers;

[ApiController]
[PermissionsRequired(nameof(PermissionId.TripView))]
[Route("api/trip")]
public class TripController : BaseController
{
    private readonly ITripService _tripService;
    
    public TripController(ITripService tripService)
    {
        _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
    }

    [HttpGet]
    public async Task<ICollection<TripModel>> GetTrips([FromQuery] GetTripRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _tripService.GetTrips(request, Caller.UserId, Caller.Permissions.Contains(nameof(PermissionId.TripViewAll)), cancellationToken);
        return response.Select(item => Mapper.Map<TripModel>(item)).ToList();
    }

    [HttpGet("{tripId:Guid}")]
    public async Task<TripModel> GetTrip([FromRoute] Guid tripId, CancellationToken cancellationToken = default)
    {
        var response = await _tripService.GetTrip(tripId, cancellationToken);
        return Mapper.Map<TripModel>(response);
    }

    [PermissionsRequired(nameof(PermissionId.TripModify))]
    [HttpPost]
    public async Task<TripModel> CreateTrip([FromBody] CreateTripRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _tripService.CreateTrip(request, Caller.UserId, cancellationToken);
        return Mapper.Map<TripModel>(response);
    }

    [PermissionsRequired(nameof(PermissionId.TripModify))]
    [HttpPut("{tripId:Guid}")]
    public async Task<TripModel> UpdateTrip([FromRoute] Guid tripId, [FromBody] TripModel request, CancellationToken cancellationToken = default)
    {
        var response = await _tripService.UpdateTrip(tripId, new(), cancellationToken);
        return Mapper.Map<TripModel>(response);
    }

    [PermissionsRequired(nameof(PermissionId.TripModify))]
    [HttpDelete("{tripId:Guid}")]
    public async Task DeleteTrip([FromRoute] Guid tripId, CancellationToken cancellationToken = default)
    {
        await _tripService.DeleteTrip(tripId, cancellationToken);
    }
}