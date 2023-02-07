using KMS.Core.Aggregates.Auth;
using KMS.Core.Aggregates.Auth.Requests;

namespace KMS.Core.Interfaces.Auth;

public interface IAuthService
{
    Task<Authentication> Authenticate(LoginRequest request, CancellationToken cancellationToken = default);
    Task Register(RegisterRequest request, CancellationToken cancellationToken = default);
}
