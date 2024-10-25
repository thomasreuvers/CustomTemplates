using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RecruitNowClient.Attributes;
using RecruitNowClient.Clients;
using RecruitNowClient.Settings;
using Refit;

namespace RecruitNowClient.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddRecruitNowClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RecruitNowSettings>(configuration.GetSection(nameof(RecruitNowSettings)));
        
        // Refit Settings (custom json serializer's can be set here)
        var refitSettings = new RefitSettings { ContentSerializer = new SystemTextJsonContentSerializer() };
        
        var clientInterfaces = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t =>
                t.IsInterface &&
                t.GetInterfaces().Any(i => i.GetCustomAttribute<RefitClientAttribute>() != null));
        
        foreach (var interfaceType in clientInterfaces)
        {
            // We have to do it like this because an interface does NOT inherit their implemented interfaces attribute's
            var attribute = interfaceType.GetInterface(nameof(IBaseClient))!.GetCustomAttribute<RefitClientAttribute>()!;
            var baseAddress = configuration.GetSection(nameof(RecruitNowSettings))
                .GetValue<string>("BaseAddress"); // Try to get from configuration
            baseAddress ??= attribute.BaseAddress;     // Fallback to attribute value

            // Ensure we have a valid base address
            if (string.IsNullOrEmpty(baseAddress))
            {
                throw new ArgumentException($"Missing BaseAddress for Refit client: {interfaceType.Name}");
            }

            services.AddRefitClient(interfaceType, refitSettings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress));

            services.TryAdd(new ServiceDescriptor(interfaceType, interfaceType, attribute.Lifetime));
        }
    }
}