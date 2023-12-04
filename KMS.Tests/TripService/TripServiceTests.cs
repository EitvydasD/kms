using KMS.Core.Aggregates.Trip;
using KMS.Core.Aggregates.Trip.Entities;
using KMS.Core.Interfaces.Trip;
using Moq;
using Utils.Library.Interfaces;

namespace KMS.Tests.TripService;

public class TripServiceTests
{
    #region Data

    protected TripEntity Trip1 => new()
    {
        Id = new Guid("406AD398-D14C-4C38-81CC-09D835B3EE1F"),
        DepartedAt = DateTime.UtcNow,
        ArrivedAt = DateTime.UtcNow,
        Status = TripStatus.Pending,
    };

    protected TripEntity Trip2 => new()
    {
        Id = new Guid("406AD398-D14C-4C38-81CC-09D835B3EE1B"),
        DepartedAt = DateTime.UtcNow,
        ArrivedAt = DateTime.UtcNow,
        Status = TripStatus.Pending,
    };

    #endregion
    protected ITripService MockServiceWithData(params TripEntity[] entities)
    {
        var mockRepo = new Mock<IRepository<TripEntity>>();
        foreach (var trip in entities)
        {
            mockRepo
                    .Setup(repo => repo.GetByIdAsync(trip.Id, CancellationToken.None))
                    .ReturnsAsync(trip);
        }

        return new Core.Services.Trip.TripService(mockRepo.Object);
    }
}
