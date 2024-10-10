using System.Reflection;

namespace VerticalSliceArchitecture;

public static class EndpointDiscovery
{
    private static readonly Type EndpointType = typeof(IEndpoint);

    public static void RegisterEndpoints(this IEndpointRouteBuilder endpoints, params Assembly[] assemblies)
    {
        if (assemblies.Length == 0)
        {
            throw new ArgumentException("At least one assembly must be provided.", nameof(assemblies));
        }

        var endpointTypes = assemblies.SelectMany(assembly => assembly.GetTypes())
            .Where(type => EndpointType.IsAssignableFrom(type) &&
                           type is { IsInterface: false, IsAbstract: false });

        foreach (var type in endpointTypes)
        {
            var method = ((IReflect)type).GetMethod(nameof(IEndpoint.MapEndpoint),
                BindingFlags.Static | BindingFlags.Public);
            method?.Invoke(null, [endpoints]);
        }
    }
}
