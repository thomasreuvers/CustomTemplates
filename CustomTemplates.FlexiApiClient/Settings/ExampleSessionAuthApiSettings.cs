using CustomTemplates.FlexiApiClient.Contracts;

namespace CustomTemplates.FlexiApiClient.Settings;

public class ExampleSessionAuthApiSettings : IBasicAuthSettings
{
    public string BaseUrl { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}