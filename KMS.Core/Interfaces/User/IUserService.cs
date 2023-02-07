using KMS.Core.Aggregates.User.Entities;
using KMS.Core.Aggregates.User.Requests;

namespace KMS.Core.Interfaces.User;

public interface IUserService
{
    Task<ICollection<UserEntity>> GetUsers(GetUserRequest request, CancellationToken cancellationToken = default);

    Task<UserEntity> GetUser(Guid userId, CancellationToken cancellationToken = default);

    Task<UserEntity> CreateUser(UserEntity request, CancellationToken cancellationToken = default);
    
    Task<UserEntity> UpdateUser(Guid callerId, Guid userId, UserEntity request, CancellationToken cancellationToken = default);

    Task DeleteUser(Guid userId, CancellationToken cancellationToken = default);
}
