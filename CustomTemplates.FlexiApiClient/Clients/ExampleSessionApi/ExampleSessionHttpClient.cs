using CustomTemplates.FlexiApiClient.Clients.Base;
using CustomTemplates.FlexiApiClient.Clients.ExampleApi;
using CustomTemplates.FlexiApiClient.Settings;
using Microsoft.Extensions.Options;
using Refit;

namespace CustomTemplates.FlexiApiClient.Clients.ExampleSessionApi;

public class ExampleSessionHttpClient(HttpClient httpClient, IOptions<ExampleSessionAuthApiSettings> settings)
    : BaseHttpClient<IExampleApi>(httpClient)
{
    public async Task<object> GetItemsAsync()
    {
        return await ExecuteWithPolicyAsync(() => Api.GetItemsAsync());
    }

    public async Task CreateItemAsync(object item)
    {
        await ExecuteWithPolicyAsync(() => Api.CreateItemAsync(item));
    }

    // Override to handle specific API exceptions
    protected override async Task HandleExceptionAsync(Exception ex)
    {
        // if (ex is HttpRequestException)
        if (ex is ApiException apiEx)
        {
            var errorContent = await apiEx.GetContentAsAsync<object>();
            // throw new CustomApiException(apiEx.StatusCode, errorContent);
            // Custom error handling for ExampleApi
        }
        else
        {
            // Fallback to base behavior
            await base.HandleExceptionAsync(ex);
        }
    }
}