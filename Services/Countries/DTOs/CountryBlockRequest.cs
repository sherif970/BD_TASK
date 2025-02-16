using System.ComponentModel.DataAnnotations;

namespace BD_TASK.Services.Countries.DTOs
{
    public class CountryBlockRequest
    {
        [Required, StringLength(2, MinimumLength = 2, ErrorMessage = "Country code must be 2 characters/ISO Alpha-2")]
        public string CountryCode { get; set; }
    }
}
