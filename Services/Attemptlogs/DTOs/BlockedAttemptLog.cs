namespace BD_TASK.Services.Logs.DTOs
{
    public class BlockedAttemptLog
    {
        public string IPAddress { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string CountryCode { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;
    }
}
