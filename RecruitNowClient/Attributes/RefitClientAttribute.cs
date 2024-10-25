using Microsoft.Extensions.DependencyInjection;

namespace RecruitNowClient.Attributes;

[AttributeUsage(
    AttributeTargets.Interface,
    AllowMultiple = false,
    Inherited = true)]
public class RefitClientAttribute : Attribute
{
    public string BaseAddress { get; set; } = string.Empty; // Optional base address
    public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Transient;
}