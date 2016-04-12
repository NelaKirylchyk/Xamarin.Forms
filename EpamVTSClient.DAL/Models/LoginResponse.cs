using EpamVTSClient.DAL.Models;

namespace EpamVTSClient.DAL.Services
{
    public class LoginResponse
    {
        public bool LoginStatus { get; set; }

        public Person Response { get; set; }
    }
}