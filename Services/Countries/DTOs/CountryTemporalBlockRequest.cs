using System.ComponentModel.DataAnnotations;

namespace BD_TASK.Services.Countries.DTOs
{
    public class CountryTemporalBlockRequest : CountryBlockRequest
    {
        [Range(1, 1440)]
        public int Minutes { get; set; }
    }
}
