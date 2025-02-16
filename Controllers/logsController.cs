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
        [HttpGet("blocked-attempts")]
        public IActionResult GetBlockedAttempts([FromQuery]LogsFilter logsFilter)
        {
            var result = logsService.GetBlockedLogs(logsFilter);
            return Ok(result);
        }

    }
}
