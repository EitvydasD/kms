using KMS.Core.Aggregates.User.Entities;

namespace KMS.Core.Aggregates.Auth.Requests;

public class RegisterRequest
{
    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public UserEntity ToEntity() => new()
    {
        Id = Guid.NewGuid(),
        Username = Username,
        FirstName = FirstName,
        LastName = LastName,
        Phone = Phone,
        Email = Email,
    };
}
