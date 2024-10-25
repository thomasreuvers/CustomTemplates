using Refit;

namespace CustomTemplates.FlexiApiClient.Clients.ExampleApi;

public interface IExampleApi
{
    [Get("/products")]
    Task<object> GetItemsAsync();

    [Post("/items")]
    Task CreateItemAsync([Body] object item);
}