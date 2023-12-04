using KMS.Core.Exceptions;

namespace KMS.Tests.UserService;
public class UpdateUserTests : UserServiceTests
{
    private Guid CallerId => TestsHelpers.SetupCallerMock(AdminUser).Object.UserId;

    [Fact]
    public async Task UpdateUser_Success()
    {
        var service = MockServiceWithData(AdminUser, User);

        var user = User;
        user.FirstName = "NewFirstName";
        user.LastName = "NewLastName";
        
        var response = await service.UpdateUser(CallerId, user.Id, user);

        Assert.True(user.Id == response.Id);
        Assert.True(user.FirstName == response.FirstName);
        Assert.True(user.LastName == response.LastName);
    }

    [Fact]
    public async Task UpdateUser_UserNotFound()
    {
        var service = MockServiceWithData(AdminUser);
        await Assert.ThrowsAsync<UserNotFoundException>(async () => await service.UpdateUser(CallerId, User.Id, User));
    }
}
