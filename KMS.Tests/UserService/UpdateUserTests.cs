namespace KMS.Tests.UserService;
public class UpdateUserTests : UserServiceTests
{
    [Fact]
    public void UpdateUser()
    {
        var user = AdminUser;

        user.Update(user.Id, new()
        {
            FirstName = "New name",
            LastName = "New last name",
            Phone = "0987654321",
        });

        Assert.True(user.Id == AdminUser.Id);
        Assert.True(user.FirstName == "New name");
        Assert.True(user.LastName == "New last name");
        Assert.True(user.Phone == "0987654321");
    }
}
