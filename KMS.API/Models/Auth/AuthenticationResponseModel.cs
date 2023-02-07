namespace KMS.API.Models.Auth;

public class AuthenticationResponseModel
{
    public AuthenticationResponseModel(string accessToken)
    {
        AccessToken = accessToken;
    }

    public string AccessToken { get; }
}
