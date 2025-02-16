using BD_TASK.Services.Logs.DTOs;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BD_TASK.Services.Logs
{
    public class LogsService : ILogsService
    {
        private static IEnumerable<BlockedAttemptLog> _blockedAttempts = [];

        public void AddBlockedLogs(BlockedAttemptLog BlockedAttemptLog)
        {
            _blockedAttempts = _blockedAttempts.Append(BlockedAttemptLog);
        }
        public PaginatedBlockedAttemptLog GetBlockedLogs(LogsFilter logsFilter)
        {
            var query = _blockedAttempts;

            if (!string.IsNullOrEmpty(logsFilter.Filter))
            {
                query = query.Where(log =>
                    log.CountryCode.Contains(logsFilter.Filter, StringComparison.OrdinalIgnoreCase));
            }

            return new PaginatedBlockedAttemptLog
            {
                Count = _blockedAttempts.Count(),
                PageSize = query.Count(),
                Page = logsFilter.Page,
                Items = query
                    .OrderByDescending(log => log.Timestamp)
                    .Skip((logsFilter.Page - 1) * logsFilter.PageSize)
                    .Take(logsFilter.PageSize)
                    .ToList()
            };
        }
    }
}
