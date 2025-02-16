using BD_TASK.Services.Ip.DTOs;
using BD_TASK.Services.Logs.DTOs;

namespace BD_TASK.Services.Ip
{
    public interface IIPService
    {
        Task<IpDataDto> Getgeolocation(string ip);
    }
}
