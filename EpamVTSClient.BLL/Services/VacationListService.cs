using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpamVTSClient.DAL.Models.DTOModels;
using EpamVTSClient.DAL.Services;
using EpamVTSClient.DAL.Services.OfflineService;
using Plugin.Connectivity;
using VtsMockClient.Domain.Models;

namespace EpamVTSClient.BLL.Services
{
    public class VacationListService : IVacationListService
    {
        private readonly IVacationListWebService _vacationListWebService;
        private readonly ILoginService _loginService;
        private readonly IVacationListOfflineDBService _vacationListOfflineDbService;

        public bool IsConnected => CrossConnectivity.Current.IsConnected;

        public VacationListService(
            IVacationListWebService vacationListWebService,
            ILoginService loginService,
            IVacationListOfflineDBService vacationListOfflineDbService)
        {
            _vacationListWebService = vacationListWebService;
            _loginService = loginService;
            _vacationListOfflineDbService = vacationListOfflineDbService;
        }

        public async Task<IEnumerable<ShortVacationInfo>> GetVacationsAsync()
        {
            int userId = _loginService.User.Id;
            if (IsConnected)
            {
                IEnumerable<ShortVacationInfo> vacationList = await _vacationListWebService.GetShortVacationsAsync(userId);
                _vacationListOfflineDbService.AddOrUpdateVacationList(userId, vacationList);
                return vacationList;
            }

            IEnumerable<VacationDTO> offlineVacationList = _vacationListOfflineDbService.GetVacationList(userId);
            List<ShortVacationInfo> shortVacationList =
                offlineVacationList.Select(vacationsDto => new ShortVacationInfo()
                {
                    Id = vacationsDto.Id,
                    Type = vacationsDto.Type,
                    Status = vacationsDto.Status,
                    StartDate = vacationsDto.StartDate,
                    EndDate = vacationsDto.EndDate
                }).ToList();
            return shortVacationList;
        }
    }

    public interface IVacationListService
    {
        Task<IEnumerable<ShortVacationInfo>> GetVacationsAsync();
    }
}
