namespace VerticalSliceArchitecture;

public class Feature : IFeature
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<Dependency>();
    }
}
