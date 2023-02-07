using KMS.API.Models.Role;
using KMS.Core.Aggregates.User.Entities;

namespace KMS.API.Models.User;

public class UserUpdateRequest : BaseUserUpdateRequest
{
    public ICollection<RoleModel> Roles { get; init; } = new List<RoleModel>();

    public override UserEntity ToEntity(Guid userId)
    {
        var entity = base.ToEntity(userId);

        entity.Roles = Roles.Select(x => new UserRoleEntity
        {
            RoleId = x.Id,
            UserId = userId,
        }).ToList();

        return entity;
    }
}
