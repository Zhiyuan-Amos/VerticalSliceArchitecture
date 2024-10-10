using System.Reflection;

namespace VerticalSliceArchitecture;

public static class FeatureDiscovery
{
    private static readonly Type ModuleType = typeof(IFeature);

    public static void ConfigureFeatures(this IServiceCollection services, IConfiguration config, params Assembly[] assemblies)
    {
        if (assemblies.Length == 0)
        {
            throw new ArgumentException("At least one assembly must be provided.", nameof(assemblies));
        }
        
        var moduleTypes = assemblies.SelectMany(assembly => assembly.GetTypes())
            .Where(type => ModuleType.IsAssignableFrom(type) &&
                        type is { IsInterface: false, IsAbstract: false });

        foreach (var type in moduleTypes)
        {
            var method = type.GetMethod(nameof(IFeature.ConfigureServices),
                BindingFlags.Static | BindingFlags.Public);
            method?.Invoke(null, [services, config]);
        }
    }
}
