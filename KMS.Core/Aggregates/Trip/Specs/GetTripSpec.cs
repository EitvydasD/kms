using Ardalis.Specification;
using KMS.Core.Aggregates.Trip.Entities;
using KMS.Core.Aggregates.Trip.Requests;

namespace KMS.Core.Aggregates.Trip.Specs;
public class GetTripSpec : Specification<TripEntity>, ISingleResultSpecification
{
    public GetTripSpec(Guid tripId)
    {
        Query
            .Include(x => x.Driver)
                .ThenInclude(x => x.Roles)
                    .ThenInclude(x => x.Role)
            .Include(x => x.Responsible)
                .ThenInclude(x => x.User)
            .Where(x => x.Id == tripId);
    }
}