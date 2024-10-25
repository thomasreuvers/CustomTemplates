using Refit;

namespace CustomTemplates.FlexiApiClient.Clients.ExampleSessionApi;

public interface IExampleSessionApi
{
    [Get("/products")]
    Task<object> GetItemsAsync();

    [Post("/items")]
    Task CreateItemAsync([Body] object item);
}