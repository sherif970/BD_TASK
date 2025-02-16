namespace BD_TASK.Services.Logs.DTOs
{
    public class PaginatedBlockedAttemptLog
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<BlockedAttemptLog> Items { get; set; }
    }
}
