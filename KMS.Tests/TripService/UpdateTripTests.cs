using KMS.Core.Aggregates.Trip;

namespace KMS.Tests.TripService;
public class UpdateTripTests : TripServiceTests
{
    [Fact]
    public void UpdateTrip()
    {
        var trip = Trip1;

        trip.Update(new()
        {
            Status = TripStatus.Delivered,
        });

        Assert.True(trip.Id == Trip1.Id);
        Assert.True(trip.Status == TripStatus.Delivered);
    }
}
