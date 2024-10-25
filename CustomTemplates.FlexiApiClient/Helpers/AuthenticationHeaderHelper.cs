using System.Net.Http.Headers;
using System.Text;

namespace CustomTemplates.FlexiApiClient.Helpers;

public static class AuthenticationHeaderHelper
{
    public static AuthenticationHeaderValue GetBasicAuthenticationHeader(string username, string password)
    {
        return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));
    }
    
    public static AuthenticationHeaderValue GetBearerAuthenticationHeader(string token)
    {
        return new AuthenticationHeaderValue("Bearer", token);
    }
}