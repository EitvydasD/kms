namespace KMS.Core.Aggregates.User.Requests;

public class ChangePasswordRequest
{
    public string CurrentPassword { get; init; } = null!;
    public string NewPassword { get; init; } = null!;
}
