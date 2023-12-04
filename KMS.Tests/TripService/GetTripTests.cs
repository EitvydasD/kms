using KMS.Core.Exceptions;

namespace KMS.Tests.TripService;
public class GetTripTests : TripServiceTests
{
    [Fact]
    public async Task GetTrip_Success()
    {
        var service = MockServiceWithData(Trip1, Trip2);

        var trip1 = await service.GetTripById(Trip1.Id);
        var trip2 = await service.GetTripById(Trip2.Id);

        Assert.True(Trip1.Id == trip1.Id);
        Assert.True(Trip2.Id == trip2.Id);
    }

    [Fact]
    public async Task GetTrip_TripNotFound()
    {
        var service = MockServiceWithData(Trip1);
        await Assert.ThrowsAsync<TripNotFoundException>(async () => await service.GetTripById(Trip2.Id));
    }
}
