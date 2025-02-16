using BD_TASK.Configurations;
using BD_TASK.Services.Countries;
using BD_TASK.Services.Ip.DTOs;
using BD_TASK.Services.Logs.DTOs;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Text.Json;

namespace BD_TASK.Services.Ip
{
    public class IPService(HttpClient httpClient, IOptions<IpGeolocationConfiguration> ipGeolocationOptions) : IIPService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _baseUrl = ipGeolocationOptions.Value.BaseUrl;
        private readonly string _apiKey = ipGeolocationOptions.Value.ApiKey;

        public async Task<IpDataDto> Getgeolocation(string ip)
        {
            var url = $"{_baseUrl}?apiKey={_apiKey}&ip={ip}";
            var response = await _httpClient.GetAsync(url);
            if(!response.IsSuccessStatusCode) return null!;
            var responseString = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<IpDataDto>(responseString, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            })!;
        }

    }
}
