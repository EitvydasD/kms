using KMS.API.Models.User;
using KMS.Core.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace KMS.API.Controllers;

[ApiController]
[Route("api/profile")]
public class ProfileController : BaseController
{
    public ProfileController(IUserService userService)
    {
        UserService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    private IUserService UserService { get; }

    [HttpGet]
    public async Task<UserModel> GetCurrentUser(CancellationToken cancellationToken = default)
    {
        var response = await UserService.GetUser(Caller.UserId, cancellationToken);
        return Mapper.Map<UserModel>(response);
    }

    [HttpGet("permissions")]
    public async Task<IEnumerable<string>> GetCurrentUserPermissions(CancellationToken cancellationToken = default)
    {
        var response = await UserService.GetUser(Caller.UserId, cancellationToken);

        return response.GetPermissions();
    }

    [HttpPut]
    public async Task<UserModel> UpdateCurrentUser([FromBody] BaseUserUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var response = await UserService.UpdateUser(Caller.UserId, Caller.UserId, request.ToEntity(Caller.UserId), cancellationToken).ConfigureAwait(false);

        return Mapper.Map<UserModel>(response);
    }
}
