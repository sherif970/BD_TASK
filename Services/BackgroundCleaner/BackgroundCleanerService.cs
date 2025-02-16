using BD_TASK.Services.Countries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BD_TASK.Services.BackgroundCleaner
{
    public class BackgroundCleanerService(IServiceProvider serviceProvider) : BackgroundService
    {
        // Injecting the ServiceProvider instead of the ICountriesService
        // because the ICountriesService is SCOPED while BackgroundCleanerService is Singleton
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var countriesService = scope.ServiceProvider.GetRequiredService<ICountriesService>();
                    countriesService.CleanupBlockedCountries();
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
