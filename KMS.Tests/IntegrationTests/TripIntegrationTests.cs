using KMS.API.Models.Auth;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;

namespace KMS.Tests.IntegrationTests;
public class TripIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TripIntegrationTests(WebApplicationFactory<Program> factory)
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
    public async Task Get_TripById_ReturnsFound()
    {
        // Arrange - authenticate and get the token
        var authToken = await GetAuthTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

        var tripId = new Guid("a79adecb-8059-487d-a0dd-253b6b10b76e");
        var apiUrl = $"/api/trip/{tripId}";

        // Act
        var response = await _client.GetAsync(apiUrl);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_TripByNonExistingId_ReturnsNotFound()
    {
        // Arrange - authenticate and get the token
        var authToken = await GetAuthTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

        Guid nonExistingTripId = Guid.Empty;
        var apiUrl = $"/api/trip/{nonExistingTripId}";

        // Act
        var response = await _client.GetAsync(apiUrl);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}