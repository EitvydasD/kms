using KMS.Core.Aggregates.Role.Entities;

namespace KMS.API.Models.Role;

public class RoleModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public IEnumerable<string>? Permissions { get; init; } = new List<string>();
    public RoleEntity ToEntity() => new()
    {
        Name = Name,
        Description = Description
    };

}
