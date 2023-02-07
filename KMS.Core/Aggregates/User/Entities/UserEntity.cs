using KMS.Core.Exceptions;
using Utils.Library.Interfaces;
using Utils.Library.Security;

namespace KMS.Core.Aggregates.User.Entities;

public class UserEntity : BaseEntity, IAggregateRoot
{
    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public DateTimeOffset PasswordChangedAt { get; set; }
    
    public byte[] PasswordSalt { get; private set; } = Array.Empty<byte>();
    
    public byte[] PasswordHash { get; private set; } = Array.Empty<byte>();
    
    public List<UserRoleEntity> Roles { get; set; } = new();
    
    public void Update(UserEntity request)
    {
        FirstName= request.FirstName;
        LastName= request.LastName;
        Phone = request.Phone;
        Email = request.Email;
        BirthDate = request.BirthDate;
    }

    public void UpdateRoles(List<UserRoleEntity> roles)
    {
        Roles = roles;
    }

    public IEnumerable<string> GetPermissions() => Roles
        .Select(x => x.Role)
        .SelectMany(x => x.Permissions)
        .Select(x => x.PermissionId)
        .Distinct();

    #region Password

    public void CheckPassword(string password)
    {
        var passwordHash = PBKDF2Password.Default.ComputeHash(password, PasswordSalt);

        if (!passwordHash.SequenceEqual(PasswordHash))
        {
            throw new IncorrectCredentialsException($"Incorrect credentials.");
        }
    }

    public void ChangePassword(string currentPassword, string newPassword)
    {
        CheckPassword(currentPassword);

        ChangePassword(newPassword);
    }

    public void ChangePassword(string password)
    {
        var hashedPassword = PBKDF2Password.Default.CreateNewPassword(password);
        PasswordHash = hashedPassword.PasswordHash;
        PasswordSalt = hashedPassword.PasswordSalt;

        PasswordChangedAt = DateTime.Now;
    }

    #endregion
}
