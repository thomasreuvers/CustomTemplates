using Polly;
using Refit;

namespace CustomTemplates.FlexiApiClient.Clients.Base;

public abstract class BaseHttpClient<TApi>
{
    protected readonly TApi Api;
    protected readonly IAsyncPolicy RetryPolicy;

    protected BaseHttpClient(HttpClient httpClient)
    {
        RetryPolicy = Policy.Handle<HttpRequestException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        
        // Initialize the Refit API
        Api = RestService.For<TApi>(httpClient);
    }
    
    // Execute a method with retry policy
    protected async Task<TResult> ExecuteWithPolicyAsync<TResult>(Func<Task<TResult>> action)
    {
        try
        {
            return await RetryPolicy.ExecuteAsync(action);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(ex);
            throw; // Re-throw after handling
        }
    }
    
    // Overload: Execute a method with retry policy for methods returning Task (i.e., no result)
    protected async Task ExecuteWithPolicyAsync(Func<Task> action)
    {
        try
        {
            await RetryPolicy.ExecuteAsync(action);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(ex);
            throw; // Re-throw after handling
        }
    }
    
    // Method to handle exceptions (we'll extend this later)
    protected virtual Task HandleExceptionAsync(Exception exception)
    {
        // Default behavior, extend for specific APIs
        throw exception;
    }
}