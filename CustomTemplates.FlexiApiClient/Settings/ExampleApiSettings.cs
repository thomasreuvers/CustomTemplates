using CustomTemplates.FlexiApiClient.Contracts;

namespace CustomTemplates.FlexiApiClient.Settings;

public class ExampleApiSettings : IBearerAuthSettings
{
    public string BaseUrl { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    
    // Add any additional settings here
}