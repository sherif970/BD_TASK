using BD_TASK.Services.Countries.DTOs;

namespace BD_TASK.Services.Countries
{
    public interface ICountriesService
    {
      
        bool IsCountryBlocked(string countryCode);
        void AddTemporalBlockedCountry(CountryTemporalBlockRequest countryCode);
        void AddBlockedCountry(CountryBlockRequest countryCode);
        void RemoveBlockedCountry(string countryCode);
        PaginatedBlockedCountries GetBlockedCountries(BlockedCountriesFilter blockedCountriesFilter);
        void CleanupBlockedCountries();
    }
}
