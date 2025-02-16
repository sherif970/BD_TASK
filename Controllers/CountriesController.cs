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

        [HttpGet("blocked")]
        public IActionResult GetBlockedCountries([FromQuery] BlockedCountriesFilter blockedCountriesFilter)
        {
            var blockedCountries = geoService.GetBlockedCountries(blockedCountriesFilter);
            return Ok(blockedCountries);
        }
    }
}