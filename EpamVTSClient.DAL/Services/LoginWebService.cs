using System;
using System.Net.Http;
using System.Threading.Tasks;
using EpamVTSClient.DAL.Extensions;
using EpamVTSClient.DAL.Models;

namespace EpamVTSClient.DAL.Services
{
    public class LoginWebService : ILoginWebService
    {
        private readonly HttpClient _client;

        public LoginWebService(HttpClient client)
        {
            _client = client;
        }

        public async Task<LoginResponse> LogInAsync(PersonCredentials model)
        {
            var loginResponse = new LoginResponse();
            try
            {
                loginResponse.Response = await _client.PostAsync<PersonCredentials, Person>(model, "login");
                loginResponse.LoginStatus = true;
            }
            catch (Exception)
            {
                loginResponse.LoginStatus = false;
            }
            return loginResponse;
        }
    }
    public interface ILoginWebService
    {
        Task<LoginResponse> LogInAsync(PersonCredentials model);
    }
}

