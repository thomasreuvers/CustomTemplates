using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Caching.Memory;

namespace CustomTemplates.FlexiApiClient.Handlers;

public abstract class BaseSessionAuthHandler<TSession>(IMemoryCache cache, string cacheKey) : DelegatingHandler
    where TSession : class
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (cache.TryGetValue(cacheKey, out TSession session))
        {
            var accessToken = GetAccessToken(session);
            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
        }
        
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var newSession = await RefreshSessionAsync();
            cache.Set(cacheKey, newSession);

            var newAccessToken = GetAccessToken(newSession);
            if (!string.IsNullOrEmpty(newAccessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);
                response = await base.SendAsync(request, cancellationToken);
            }
        }

        return response;
    }
    
    protected abstract Task<TSession> RefreshSessionAsync();
    protected abstract string GetAccessToken(TSession session);
}