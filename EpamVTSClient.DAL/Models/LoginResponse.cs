namespace EpamVTSClient.DAL.Models
{
    public class LoginResponse
    {
        public bool LoginStatus { get; set; }

        public Person Response { get; set; }
    }
}