using KMS.Core.Exceptions;
using Utils.Library.Security;

namespace KMS.Tests.UserService;
public class ChangePasswordTest : UserServiceTests
{
    private readonly string Password1 = "Password123";
    private readonly string Password2 = "Password123";

    [Fact]
    public async Task ChangePassword_Success()
    {
        AdminUser.ChangePassword(Password1);
        
        AdminUser.ChangePassword(Password2);

        var passwordHash = PBKDF2Password.Default.ComputeHash(Password1, AdminUser.PasswordSalt);

        AdminUser.CheckPassword(Password2);
    }
}
