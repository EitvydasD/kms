using KMS.Core.Aggregates.User.Entities;
using KMS.Core.Aggregates.User.Specs;
using KMS.Core.Interfaces.Comment;
using KMS.Core.Interfaces.User;
using KMS.Core.Exceptions;
using Utils.Library.Interfaces;
using KMS.Core.Aggregates.User.Requests;
using System.Data;

namespace KMS.Core.Services.User;

public class UserService : IUserService
{
    public UserService(IRepository<UserEntity> userRepo, ICommentService commentService)
    {
        UserRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        CommentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
    }

    private IRepository<UserEntity> UserRepo { get; }
    private ICommentService CommentService { get; }

    public async Task<ICollection<UserEntity>> GetUsers(GetUserRequest request, CancellationToken cancellationToken = default)
    {
        var spec = new GetUsersSpec(request);
        return await UserRepo.ListAsync(spec, cancellationToken);
    }

    public async Task<UserEntity> GetUser(Guid userId, CancellationToken cancellationToken = default)
    {
        var role = await UserRepo.FirstOrDefaultAsync(new GetUserSpec(userId), cancellationToken);

        if (role is null)
        {
            throw new UserNotFoundException(userId);
        }

        return role;
    }

    public async Task<UserEntity> GetUserById(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await UserRepo.GetByIdAsync(userId, cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException(userId);
        }

        return user;
    }

    public async Task<UserEntity> CreateUser(UserEntity request, CancellationToken cancellationToken = default)
    {
        return await UserRepo.AddAsync(request, cancellationToken);
    }

    public async Task<UserEntity> UpdateUser(Guid callerId, Guid userId, UserEntity request, CancellationToken cancellationToken = default)
    {
        var user = await GetUser(userId, cancellationToken);

        user.Update(callerId, request);

        await UserRepo.SaveChangesAsync(cancellationToken);
        return user;
    }
    
    public async Task DeleteUser(Guid userId, CancellationToken cancellationToken = default)
    {
        await CommentService.DeleteUserComments(userId, cancellationToken);
        await UserRepo.DeleteAsync(await GetUser(userId, cancellationToken), cancellationToken);
    }
}
