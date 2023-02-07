using KMS.API.Models.Role;
using KMS.Core.Aggregates.User.Models;

namespace KMS.API.Models.User;

public class UserModel : BaseUserModel
{
    public ICollection<RoleModel> Roles { get; set; } = new List<RoleModel>();
}