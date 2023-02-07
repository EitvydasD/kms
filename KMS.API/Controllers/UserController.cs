using KMS.API.Models.User;
using KMS.Core.Aggregates.Role;
using KMS.Core.Interfaces.User;
using KMS.Core;
using Microsoft.AspNetCore.Mvc;
using KMS.Core.Aggregates.User.Requests;

namespace KMS.API.Controllers;

[ApiController]
[PermissionsRequired(nameof(PermissionId.UserView))]
[Route("api/user")]
public class UserController : BaseController
{
    public UserController(IUserService userService)
    {
        UserService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    private IUserService UserService { get; }

    [HttpGet]
    public async Task<ICollection<UserModel>> GetUsers([FromQuery] GetUserRequest request, CancellationToken cancellationToken = default)
    {
        var response = await UserService.GetUsers(request, cancellationToken).ConfigureAwait(false);
        return Mapper.Map<ICollection<UserModel>>(response);
    }

    [HttpGet("{userId:Guid}")]
    public async Task<UserModel> GetUser([FromRoute] Guid userId, CancellationToken cancellationToken = default)
    {
        var response = await UserService.GetUser(userId,cancellationToken).ConfigureAwait(false);
        return Mapper.Map<UserModel>(response);
    }

    [PermissionsRequired(nameof(PermissionId.UserModify))]
    [HttpPut("{userId:Guid}")]
    public async Task<UserModel> UpdateUser([FromRoute] Guid userId, [FromBody] UserUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var response = await UserService.UpdateUser(Caller.UserId, userId, request.ToEntity(userId), cancellationToken).ConfigureAwait(false);

        return Mapper.Map<UserModel>(response);
    }

    [PermissionsRequired(nameof(PermissionId.UserModify))]
    [HttpDelete("{userId:Guid}")]
    public async Task DeleteUser([FromRoute] Guid userId, CancellationToken cancellationToken = default)
    {
        await UserService.DeleteUser(userId, cancellationToken).ConfigureAwait(false);
    }
}
