using BD_TASK.Services.Countries;
using BD_TASK.Services.Ip;
using BD_TASK.Services.Logs;
using BD_TASK.Services.Logs.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BD_TASK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IPController(ICountriesService geoService, ILogsService logsService, IIPService iPService)
        : ControllerBase
    {
        [HttpGet("lookup")]
        public async Task<IActionResult> GetLocation(string? ip)
        {
            //if (string.IsNullOrEmpty(ip))
            //{
            //    ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            //    if (string.IsNullOrEmpty(ip) || ip == "::1")
            //    {
            //        ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
            //            ?? HttpContext.Request.Headers["CF-Connecting-IP"].FirstOrDefault()
            //            ?? HttpContext.Request.Headers["X-Real-IP"].FirstOrDefault();
            //    }
            //}
            // trying to get public IP but it in all cases will be null
            // so the thrid-party service will use the client's IP address
            var locationData = await iPService.Getgeolocation(ip);
            if (locationData == null)
            {
                return NotFound(new { message = "Location data not found for the provided IP." });
            }
            if (geoService.IsCountryBlocked(locationData.CountryCode2))
            {
                return Conflict(new { message = $"Country '{locationData.CountryCode2}' is blocked." });
            }
            return Ok(locationData);
        }
        [HttpGet("check-block")]
        public async Task<IActionResult> CheckIfBlocked()
        {
            var locationData = await iPService.Getgeolocation(null!);
            if (geoService.IsCountryBlocked(locationData.CountryCode2))
            {
                string userAgent = HttpContext.Request.Headers.UserAgent.FirstOrDefault() ?? "Unknown";
                BlockedAttemptLog log = new()
                {
                    IPAddress = locationData.Ip,
                    CountryCode = locationData.CountryCode2,
                    UserAgent = userAgent,
                    Timestamp = DateTime.Now
                };
                logsService.AddBlockedLogs(log);
                return Conflict(new { message = $"your '{locationData.CountryCode2}' Country is blocked." });
            }
            return Ok(new { message = $"your '{locationData.CountryCode2}' Country is not blocked." });
        }
    }
}
