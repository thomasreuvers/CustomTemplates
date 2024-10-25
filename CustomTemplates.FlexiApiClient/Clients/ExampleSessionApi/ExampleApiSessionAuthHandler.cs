using System.Net.Http.Json;
using CustomTemplates.FlexiApiClient.Handlers;
using CustomTemplates.FlexiApiClient.Settings;
using Microsoft.Extensions.Caching.Memory;

namespace CustomTemplates.FlexiApiClient.Clients.ExampleSessionApi;

public class ExampleApiSessionAuthHandler(
    IMemoryCache cache,
    ExampleSessionAuthApiSettings exampleApiSettings,
    IHttpClientFactory clientFactory) 
    : BaseSessionAuthHandler<ExampleApiSession>(cache, nameof(ExampleApiSession))
{
    protected override async Task<ExampleApiSession> RefreshSessionAsync()
    {
        var client = clientFactory.CreateClient("ExampleApi");

        HttpRequestMessage request;

        if (cache.TryGetValue(nameof(ExampleApiSession), out ExampleApiSession? session))
        {
            // We have a session, so we're refreshing it
            request = new HttpRequestMessage(HttpMethod.Post, "token/refresh")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "refresh", session?.RefreshToken }
                })
            };
        }
        else
        {
            // We're getting a new session
            request = new HttpRequestMessage(HttpMethod.Post, "token/login")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "username", exampleApiSettings.Username },
                    { "password", exampleApiSettings.Password }
                })
            };
        }

        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var newSession = await response.Content.ReadFromJsonAsync<ExampleApiSession>();
        return newSession ?? throw new InvalidOperationException("Failed to refresh session.");
    }

    protected override string GetAccessToken(ExampleApiSession session)
    {
        return session.AccessToken;
    }
}