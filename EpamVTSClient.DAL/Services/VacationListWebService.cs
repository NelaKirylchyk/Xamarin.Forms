using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EpamVTSClient.DAL.Extensions;
using EpamVTSClient.DAL.Models;

namespace EpamVTSClient.DAL.Services
{
    public class VacationListWebService : IVacationListWebService
    {
        private readonly HttpClient _client;

        public VacationListWebService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<ShortVacationInfo>> GetShortVacationsAsync(int userId)
        {
            try
            {
                List<ShortVacationInfo> vacationsResponse = await _client.GetAsync<List<ShortVacationInfo>>($"vacation/list/get?id={userId}");
                return vacationsResponse;
            }
            catch (Exception)
            {
                return Enumerable.Empty<ShortVacationInfo>();
            }
        }
    }

    public interface IVacationListWebService
    {
        Task<IEnumerable<ShortVacationInfo>> GetShortVacationsAsync(int userId);
    }
}
