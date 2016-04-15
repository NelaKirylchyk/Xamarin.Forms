namespace EpamVTSClient.DAL.Models
{
    public class LoginResponse
    {
        public bool IsLogedIn { get; set; }

        public Person Response { get; set; }
    }
}