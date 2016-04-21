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

        public async Task<VacationInfo> GetFullVacationInfoAsync(int vacationId)
        {
            try
            {
                var vacationResponse = await _client.GetAsync<VacationInfo>($"vacation/get/get?id={vacationId}");
                return vacationResponse;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> AddUpdateVacationAsync(VacationInfo vacationInfo)
        {
            try
            {
                int newVacationId = await _client.PostAsync<VacationInfo, int>(vacationInfo, "vacation/update");
                if (newVacationId > 0)
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
    }
}
