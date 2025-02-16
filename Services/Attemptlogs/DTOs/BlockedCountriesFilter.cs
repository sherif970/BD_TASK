using System.ComponentModel.DataAnnotations;

namespace BD_TASK.Services.Logs.DTOs
{
    public class LogsFilter
    {
        public string? Filter { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Page must be at least 1.")]
        public int Page { get; set; } = 1;
        [Range(1, int.MaxValue, ErrorMessage = "Pagesize must be at least 1.")]
        public int PageSize { get; set; } = 10;
    }
}
