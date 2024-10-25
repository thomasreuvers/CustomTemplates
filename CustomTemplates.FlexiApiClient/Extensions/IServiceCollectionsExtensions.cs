using CustomTemplates.FlexiApiClient.Clients.ExampleApi;
using CustomTemplates.FlexiApiClient.Clients.ExampleSessionApi;
using CustomTemplates.FlexiApiClient.Contracts;
using CustomTemplates.FlexiApiClient.Handlers;
using CustomTemplates.FlexiApiClient.Helpers;
using CustomTemplates.FlexiApiClient.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CustomTemplates.FlexiApiClient.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddFlexiApiClients(this IServiceCollection services, IConfiguration configuration)
    {
        // Explicitly register API clients and their settings
        RegisterApiClient<ExampleHttpClient, ExampleApiSettings>(services, configuration);
        RegisterApiClientWithSessionAuth<ExampleHttpClient, ExampleApiSettings, ExampleApiSessionAuthHandler, ExampleApiSession>(services, configuration);
    }

    private static void RegisterApiClient<TClient, TSettings>(IServiceCollection services, IConfiguration configuration)
        where TClient : class
        where TSettings : class, ISettings
    {
        // Register the settings for the API client
        var sectionName = typeof(TSettings).Name.Replace("Settings", string.Empty);
        services.Configure<TSettings>(configuration.GetSection($"ApiSettings:{sectionName}"));

        // Register the HttpClient for the API client
        services.AddHttpClient<TClient>((provider, client) =>
        {
            var settings = provider.GetRequiredService<IOptions<TSettings>>().Value;

            // Assuming TSettings has BaseUrl and AuthenticationType properties
            client.BaseAddress = new Uri(settings.BaseUrl);

            // Set up authentication based on the settings
            client.DefaultRequestHeaders.Authorization = settings switch
            {
                IBearerAuthSettings bearerAuthSettings => AuthenticationHeaderHelper.GetBearerAuthenticationHeader(bearerAuthSettings.Token),
                IBasicAuthSettings basicAuthSettings => AuthenticationHeaderHelper.GetBasicAuthenticationHeader(basicAuthSettings.Username, basicAuthSettings.Password),
                _ => null
            };
        });
    }

    private static void RegisterApiClientWithSessionAuth<TClient, TSettings, TAuthHandler, TSession>(IServiceCollection services,
        IConfiguration configuration)
        where TClient : class
        where TSettings : class, ISettings
        where TAuthHandler : BaseSessionAuthHandler<TSession>
        where TSession : class
    {
        // Register the settings for the API client
        var sectionName = typeof(TSettings).Name.Replace("Settings", string.Empty);
        services.Configure<TSettings>(configuration.GetSection($"ApiSettings:{sectionName}"));
        
        // Register the HttpClient for the API client
        services.AddHttpClient<TClient>((provider, client) =>
        {
            var settings = provider.GetRequiredService<IOptions<TSettings>>().Value;

            // Assuming TSettings has BaseUrl and AuthenticationType properties
            client.BaseAddress = new Uri(settings.BaseUrl);
        })
        .AddHttpMessageHandler<TAuthHandler>();
        
        // Register the authentication handler
        services.AddScoped<TAuthHandler>();
    }
}