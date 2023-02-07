using JetBrains.Annotations;
using KMS.Core.Aggregates.User.Entities;

namespace KMS.API.Models.User;

public class BaseUserUpdateRequest
{
    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string Phone { get; init; } = null!;

    public DateOnly BirthDate { get; init; }

    public virtual UserEntity ToEntity(Guid userId) => new()
    {
        FirstName = FirstName,
        LastName = LastName,
        Phone = Phone,
        Email = Email,
        BirthDate = BirthDate,
    };
}
