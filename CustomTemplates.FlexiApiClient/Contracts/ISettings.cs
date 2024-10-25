namespace CustomTemplates.FlexiApiClient.Contracts;

public interface ISettings
{
    public string BaseUrl { get; set; }
}

public interface IBasicAuthSettings : ISettings
{
    public string Username { get; set; }
    
    public string Password { get; set; }
}

public interface IBearerAuthSettings : ISettings
{
    public string Token { get; set; }
}