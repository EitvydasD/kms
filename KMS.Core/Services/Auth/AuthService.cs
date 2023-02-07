using KMS.Core.Aggregates.Auth;
using KMS.Core.Aggregates.Auth.Requests;
using KMS.Core.Aggregates.Role.Entities;
using KMS.Core.Aggregates.Role.Filters;
using KMS.Core.Aggregates.Role.Specs;
using KMS.Core.Aggregates.User.Entities;
using KMS.Core.Aggregates.Users.Specs;
using KMS.Core.Interfaces.Auth;
using KMS.Core.Exceptions;
using Utils.Library.Interfaces;

namespace KMS.Core.Services.Auth;

public class AuthService : IAuthService
{
    public AuthService(IRepository<UserEntity> userRepo, IReadRepository<RoleEntity> roleRepo)
    {
        UserRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        RoleRepo = roleRepo ?? throw new ArgumentNullException(nameof(roleRepo));
    }

    private IRepository<UserEntity> UserRepo { get; }
    private IReadRepository<RoleEntity> RoleRepo { get; }

    public async Task<Authentication> Authenticate(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await GetUser(request.Username, cancellationToken).ConfigureAwait(false);

        user.CheckPassword(request.Password);

        return new(user);
    }

    public async Task Register(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var roles = await RoleRepo.ListAsync(new GetRolesSpec(new GetRoleFilter { IsDefault = true }), cancellationToken);

        var user = request.ToEntity();

        user.ChangePassword(request.Password);
        user.Roles = roles.Select(x => new UserRoleEntity { RoleId = x.Id, UserId = user.Id }).ToList();

        await UserRepo.AddAsync(user, cancellationToken);
    }

    private async Task<UserEntity> GetUser(string username, CancellationToken cancellationToken = default)
    {
        var spec = new GetUserByUsernameSpec(username);
        var user = await UserRepo.FirstOrDefaultAsync(spec, cancellationToken) ?? throw new IncorrectCredentialsException("User not found");

        return user;
    }
}
