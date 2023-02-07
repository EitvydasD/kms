using KMS.Core.Aggregates.User.Entities;

namespace KMS.Core.Aggregates.User.Models;

public class BaseUserModel
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public BaseUserModel()
    {

    }

    public BaseUserModel(UserEntity user)
    {
        Id = user.Id;
        Username = user.Username;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Phone = user.Phone;
        Email = user.Email;
        BirthDate = user.BirthDate;
    }
}
