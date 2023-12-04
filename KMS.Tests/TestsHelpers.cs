using KMS.Core.Aggregates.User.Entities;
using KMS.Core.Interfaces;
using Moq;

namespace KMS.Tests;
public static class TestsHelpers
{
    public static Mock<ICallerAccessor> SetupCallerMock(UserEntity user)
    {
        var callerMock = new Mock<ICallerAccessor>();
        callerMock.Setup(x => x.UserId).Returns(user.Id);

        return callerMock;
    }
}
