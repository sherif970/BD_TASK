using BD_TASK.Services.Ip;
using BD_TASK.Services.Logs;
using BD_TASK.Services.Logs.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BD_TASK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController(ILogsService logsService) : ControllerBase
    {
        /// <summary>
        /// Retrieves a paginated list of blocked access attempts.
        /// </summary>
        /// <param name="logsFilter">Filter parameters for pagination and searching.</param>
        /// <returns>A list of blocked attempts including IP address, timestamp, country code, blocked status, and user agent.</returns>
        [HttpGet("blocked-attempts")]
        public IActionResult GetBlockedAttempts([FromQuery]LogsFilter logsFilter)
        {
            var result = logsService.GetBlockedLogs(logsFilter);
            return Ok(result);
        }

    }
}
