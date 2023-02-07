using KMS.API.Models.Trip;
using KMS.Core;
using KMS.Core.Aggregates.Role;
using KMS.Core.Aggregates.Trip.Requests;
using KMS.Core.Interfaces.Trip;
using Microsoft.AspNetCore.Mvc;

namespace KMS.API.Controllers;

[ApiController]
[PermissionsRequired(nameof(PermissionId.TripView))]
[Route("api/trip")]
public class TripController : BaseController
{
    public TripController(ITripService tripService)
    {
        TripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
    }
    
    private ITripService TripService { get; }

    [HttpGet]
    public async Task<ICollection<TripModel>> GetTrips([FromQuery] GetTripRequest request, CancellationToken cancellationToken = default)
    {
        var response = await TripService.GetTrips(request, Caller.UserId, Caller.Permissions.Contains(nameof(PermissionId.TripViewAll)), cancellationToken);

        var model = new List<TripModel>();

        foreach (var item in response)
        {
            model.Add(new TripModel
            {
                Id = item.Id,
                Driver = new(item.Driver),
                DepartedAt = item.DepartedAt,
                ArrivedAt = item.ArrivedAt,
                Status = item.Status,
            });
        }

        return model;
    }

    [HttpGet("{tripId:Guid}")]
    public async Task<TripModel> GetTrip([FromRoute] Guid tripId, CancellationToken cancellationToken = default)
    {
        var response = await TripService.GetTrip(tripId, cancellationToken);
        return Mapper.Map<TripModel>(response);
    }
    
    [PermissionsRequired(nameof(PermissionId.TripModify))]
    [HttpPost]
    public async Task<TripModel> CreateTrip([FromBody] CreateTripRequest request, CancellationToken cancellationToken = default)
    {
        var response = await TripService.CreateTrip(request, Caller.UserId, cancellationToken);
        return Mapper.Map<TripModel>(response);
    }

    [PermissionsRequired(nameof(PermissionId.TripModify))]
    [HttpPut("{tripId:Guid}")]
    public async Task<TripModel> UpdateTrip([FromRoute] Guid tripId, [FromBody] TripModel request, CancellationToken cancellationToken = default)
    {
        var response = await TripService.UpdateTrip(tripId, request.ToEntity(tripId), cancellationToken);
        return Mapper.Map<TripModel>(response);
    }

    [PermissionsRequired(nameof(PermissionId.TripModify))]
    [HttpDelete("{tripId:Guid}")]
    public async Task DeleteTrip([FromRoute] Guid tripId, CancellationToken cancellationToken = default)
    {
        await TripService.DeleteTrip(tripId, cancellationToken);
    }
}
