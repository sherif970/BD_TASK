using BD_TASK.Services.Logs.DTOs;

namespace BD_TASK.Services.Logs
{
    public interface ILogsService
    {
        void AddBlockedLogs(BlockedAttemptLog BlockedAttemptLog);
        PaginatedBlockedAttemptLog GetBlockedLogs(LogsFilter logsFilter);
    }
}
