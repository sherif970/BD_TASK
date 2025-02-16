using BD_TASK.Services.Countries;
using BD_TASK.Services.Countries.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BD_TASK.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController(ICountriesService geoService) : ControllerBase
    {
        /// <summary>
        /// Temporarily blocks a country based on the provided request.
        /// </summary>
        /// <param name="request">The request containing the country code to block.</param>
        /// <returns>
        /// Returns a 409 Conflict response if the country is already blocked.
        /// Returns a 204 No Content response if the country is successfully blocked.
        /// </returns>
        [HttpPost("temporal-block")]
        public IActionResult Block([FromBody] CountryTemporalBlockRequest request)
        {
            if (geoService.IsCountryBlocked(request.CountryCode))
            {
                return Conflict(new { message = $"Country '{request.CountryCode}' is already blocked." });
            }

            geoService.AddTemporalBlockedCountry(request);
            return NoContent();
        }

        /// <summary>
        /// Permanently blocks a country based on the provided request.
        /// </summary>
        /// <param name="request">The request containing the country code to block.</param>
        /// <returns>
        /// Returns a 409 Conflict response if the country is already blocked.
        /// Returns a 204 No Content response if the country is successfully blocked.
        /// </returns>
        [HttpPost("Block")]
        public IActionResult Block([FromBody] CountryBlockRequest request)
        {
            if (geoService.IsCountryBlocked(request.CountryCode))
            {
                return Conflict(new { message = $"Country '{request.CountryCode}' is already blocked." });
            }

            geoService.AddBlockedCountry(request);
            return NoContent();
        }

        /// <summary>
        /// Removes a country from the blocked list.
        /// </summary>
        /// <param name="request">The request containing the country code to unblock.</param>
        /// <returns>
        /// Returns a 404 Not Found response if the country is not currently blocked.  
        /// Returns a 204 No Content response if the country is successfully unblocked.  
        /// </returns>
        [HttpDelete("Block")]
        public IActionResult Removeblock([FromBody] CountryBlockRequest request)
        {
            if (!geoService.IsCountryBlocked(request.CountryCode))
            {
                return NotFound(new { message = $"Country '{request.CountryCode}' is not blocked." });
            }

            geoService.RemoveBlockedCountry(request.CountryCode);
            return NoContent();
        }

        /// <summary>
        /// Retrieves a paginated list of blocked countries.
        /// </summary>
        /// <param name="blockedCountriesFilter">Filter parameters including pagination and search criteria.</param>
        /// <returns>
        /// Returns a 200 OK response with a list of blocked countries.
        /// </returns>
        [HttpGet("blocked")]
        public IActionResult GetBlockedCountries([FromQuery] BlockedCountriesFilter blockedCountriesFilter)
        {
            var blockedCountries = geoService.GetBlockedCountries(blockedCountriesFilter);
            return Ok(blockedCountries);
        }
    }
}