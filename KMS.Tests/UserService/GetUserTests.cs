using KMS.Core.Exceptions;

namespace KMS.Tests.UserService;
public class GetUserTests : UserServiceTests
{
    [Fact]
    public async Task GetUser_Success()
    {
        var service = MockServiceWithData(AdminUser, User);

        var adminUser = await service.GetUserById(AdminUser.Id);
        var user = await service.GetUserById(User.Id);

        Assert.True(AdminUser.Id == adminUser.Id);
        Assert.True(User.Id == user.Id);
    }

    [Fact]
    public async Task GetUser_UserNotFound()
    {
        var service = MockServiceWithData(AdminUser);
        await Assert.ThrowsAsync<UserNotFoundException>(async () => await service.GetUserById(User.Id));
    }
}
