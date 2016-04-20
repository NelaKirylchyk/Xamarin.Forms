using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpamVTSClient.Core.Services;
using EpamVTSClient.DAL.Models;
using EpamVTSClient.DAL.Models.DTOModels;
using EpamVTSClient.DAL.Services;
using EpamVTSClient.DAL.Services.OfflineService;
using Plugin.Connectivity;

namespace EpamVTSClient.BLL.Services
{
    public class VacationListService : IVacationListService
    {
        private readonly IVacationListWebService _vacationListWebService;
        private readonly ILoginService _loginService;
        private readonly IVacationListOfflineDBService _vacationListOfflineDbService;
        private readonly IMessageDialogService _messageDialogService;

        public bool IsConnected => CrossConnectivity.Current.IsConnected;

        public VacationListService(
            IVacationListWebService vacationListWebService,
            ILoginService loginService,
            IVacationListOfflineDBService vacationListOfflineDbService,
            IMessageDialogService messageDialogService)
        {
            _vacationListWebService = vacationListWebService;
            _loginService = loginService;
            _vacationListOfflineDbService = vacationListOfflineDbService;
            _messageDialogService = messageDialogService;
        }

        public async Task<IEnumerable<ShortVacationInfo>> GetVacationsAsync()
        {
            int userId = _loginService.User.Id;
            if (IsConnected)
            {
                IEnumerable<ShortVacationInfo> vacationList = await _vacationListWebService.GetShortVacationsAsync(userId);
                await _vacationListOfflineDbService.AddOrUpdateVacationListAsync(userId, vacationList);
                return vacationList;
            }

            IEnumerable<VacationDTO> offlineVacationList = await _vacationListOfflineDbService.GetVacationListAsync(userId);
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

        public async Task<VacationInfo> GetFullVacationInfoAsync(int vacationId)
        {
            try
            {
                if (IsConnected)
                {
                    var vacationInfo = await _vacationListWebService.GetFullVacationInfoAsync(vacationId);
                    if (vacationInfo != null)
                    {
                        await _vacationListOfflineDbService.AddUpdateFullVacationInfoAsync(vacationInfo);
                    }
                    return vacationInfo;
                }
                var fullVacationDto = await _vacationListOfflineDbService.GetFullVacationAsync(vacationId);
                var fullVacationInfo = new VacationInfo();
                fullVacationInfo.Update(fullVacationDto);

                return fullVacationInfo;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task AddUpdateVacationInfoAsync(VacationInfo vacationInfo)
        {
            try
            {
                if (IsConnected)
                {
                    var isUpdated = await _vacationListWebService.AddUpdateVacationAsync(vacationInfo);
                    if (isUpdated)
                    {
                        await _vacationListOfflineDbService.AddUpdateFullVacationInfoAsync(vacationInfo);
                    }
                }
                await _vacationListOfflineDbService.AddUpdateFullVacationInfoAsync(vacationInfo);
            }
            catch (Exception e)
            {
                await _messageDialogService.ShowMessageDialogAsync(e.Message);
            }
        }
    }

    public interface IVacationListService
    {
        Task<IEnumerable<ShortVacationInfo>> GetVacationsAsync();
        Task<VacationInfo> GetFullVacationInfoAsync(int vacationId);

        Task AddUpdateVacationInfoAsync(VacationInfo vacationInfo);
    }
}
