using BD_TASK.Services.Logs.DTOs;

namespace BD_TASK.Services.Countries.DTOs
{
    public class PaginatedBlockedCountries
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<string> Items { get; set; }
    }
}
