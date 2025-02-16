using BD_TASK.Services.BackgroundCleaner;
using BD_TASK.Services.Countries;
using BD_TASK.Services.Ip;
using BD_TASK.Services.Logs;

namespace BD_TASK.Configurations
{
    public static class ServiesCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped<ICountriesService, CountriesService>()
                .AddScoped<IIPService, IPService>()
                .AddScoped<ILogsService, LogsService>()
                .AddHostedService<BackgroundCleanerService>()
                .Configure<IpGeolocationConfiguration>(configuration.GetSection("IpGeolocation"));
        }
    }
}
