using Ardalis.Specification;
using KMS.Core.Aggregates.Trip.Entities;
using KMS.Core.Aggregates.Trip.Requests;

namespace KMS.Core.Aggregates.Trip.Specs;
public class GetTripsSpec : Specification<TripEntity>
{
    public GetTripsSpec(GetTripRequest request, Guid callerId, bool hasPermissionViewAll)
    {
        if (request.DriverId.HasValue)
        {
            Query
                .Where(x => x.DriverId == request.DriverId);
        }

        if (request.Status.HasValue)
        {
            Query
                .Where(x => x.Status == request.Status);
        }

        if (request.DepartedAt.HasValue)
        {
            Query
                .Where(x => x.DepartedAt >= request.DepartedAt);

        }

        if (request.ArrivedAt.HasValue)
        {
            Query
                .Where(x => x.ArrivedAt <= request.ArrivedAt);
        }

        if (!hasPermissionViewAll)
        {
            Query.Where(x => x.Responsible.Any(x => x.UserId == callerId) || x.Responsible.Count == 0);
        }

        Query
            .Include(x => x.Driver)
                .ThenInclude(x => x.Roles)
                    .ThenInclude(x => x.Role)
            .OrderBy(x => x.ArrivedAt);
    }
}