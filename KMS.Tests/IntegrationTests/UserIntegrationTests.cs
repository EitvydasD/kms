using KMS.API.Models.Auth;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;

namespace KMS.Tests.IntegrationTests;
public class UserIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UserIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    private async Task<string> GetAuthTokenAsync()
    {
        var loginUrl = "/api/auth/login";
        var loginData = new
        {
            Username = "admin",
            Password = "admin"
        };

        var response = await _client.PostAsJsonAsync(loginUrl, loginData);

        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadAsAsync<AuthenticationResponseModel>();
        return responseData.AccessToken;
    }

    [Fact]
    public async Task Post_DeleteAllMessagesHandler_ReturnsRedirectToRoot()
    {
        // Arrange - authenticate and get the token
        var authToken = await GetAuthTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
    }

    [Fact]
    public async Task Get_UserById_ReturnsFound()
    {
        // Arrange - authenticate and get the token
        var authToken = await GetAuthTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

        var userId = new Guid("562f1a9a-066b-4788-94a3-e0a552b599a3");
        var apiUrl = $"/api/user/{userId}";

        // Act
        var response = await _client.GetAsync(apiUrl);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_UserByNonExistingId_ReturnsNotFound()
    {
        // Arrange - authenticate and get the token
        var authToken = await GetAuthTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

        var nonExistingUserId = Guid.Empty;
        var apiUrl = $"/api/user/{nonExistingUserId}";

        // Act
        var response = await _client.GetAsync(apiUrl);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}