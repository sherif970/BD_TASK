
using BD_TASK.Configurations;
using BD_TASK.Services.Countries.DTOs;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.Json;

namespace BD_TASK.Services.Countries
{
    public class CountriesService() : ICountriesService
    {
        private static ConcurrentDictionary<string, DateTime?> _blockedCountries = new ConcurrentDictionary<string, DateTime?>(StringComparer.OrdinalIgnoreCase);

        public void AddTemporalBlockedCountry(CountryTemporalBlockRequest temporalBlockRequest)
        {
            if (!_blockedCountries.ContainsKey(temporalBlockRequest.CountryCode))
            {
                _blockedCountries[temporalBlockRequest.CountryCode] = DateTime.Now.AddMinutes(temporalBlockRequest.Minutes);
            }
        }
        public void AddBlockedCountry(CountryBlockRequest blockRequest)
        {
            if (!_blockedCountries.ContainsKey(blockRequest.CountryCode))
            {
                _blockedCountries[blockRequest.CountryCode] = null;
            }
        }
        public PaginatedBlockedCountries GetBlockedCountries(BlockedCountriesFilter blockedCountriesFilter)
        {
            IEnumerable<string> query = _blockedCountries.Keys;

            if (!string.IsNullOrWhiteSpace(blockedCountriesFilter.Filter))
            {
                query = query.Where(c =>
                    c.Contains(blockedCountriesFilter.Filter, StringComparison.OrdinalIgnoreCase));
            }

            return new PaginatedBlockedCountries
            {
                Count = _blockedCountries.Count,
                Page = blockedCountriesFilter.Page,
                PageSize = blockedCountriesFilter.PageSize,
                Items = query
                .Skip((blockedCountriesFilter.Page - 1) * blockedCountriesFilter.PageSize)
                .Take(blockedCountriesFilter.PageSize)
                .ToList()
            };
        }
        public void RemoveBlockedCountry(string countryCode)
        {
            _blockedCountries.TryRemove(countryCode, out _);
        }
        public bool IsCountryBlocked(string countryCode)
        {
            return _blockedCountries.ContainsKey(countryCode);
        }
        public void CleanupBlockedCountries()
        {
            var expiredKeys = _blockedCountries
                    .Where(kvp => kvp.Value <= DateTime.Now)
                    .Select(kvp => kvp.Key)
                    .ToList();

            expiredKeys.ForEach(item => _blockedCountries.TryRemove(item, out _));
        }

    }
}
