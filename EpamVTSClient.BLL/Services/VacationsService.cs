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
    public class VacationsService : IVacationsService
    {
        private readonly IVacationsWebService _vacationsWebService;
        private readonly ILoginService _loginService;
        private readonly IVacationListOfflineDBService _vacationListOfflineDbService;
        private readonly IMessageDialogService _messageDialogService;

        public bool IsConnected => CrossConnectivity.Current.IsConnected;

        public VacationsService(
            IVacationsWebService vacationsWebService,
            ILoginService loginService,
            IVacationListOfflineDBService vacationListOfflineDbService,
            IMessageDialogService messageDialogService)
        {
            _vacationsWebService = vacationsWebService;
            _loginService = loginService;
            _vacationListOfflineDbService = vacationListOfflineDbService;
            _messageDialogService = messageDialogService;
        }

        public async Task<IEnumerable<ShortVacationInfo>> GetVacationsAsync()
        {
            int userId = _loginService.User.Id;
            if (IsConnected)
            {
                IEnumerable<ShortVacationInfo> vacationList = await _vacationsWebService.GetShortVacationsAsync(userId);
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
                    var vacationInfo = await _vacationsWebService.GetFullVacationInfoAsync(vacationId);
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
                    int id = await _vacationsWebService.AddUpdateVacationAsync(vacationInfo);
                    if (id > 0)
                    {
                        vacationInfo.Id = id;
                        await _vacationListOfflineDbService.AddUpdateFullVacationInfoAsync(vacationInfo);
                    }
                }
                else
                {
                    List<VacationDTO> vacationDtos = await _vacationListOfflineDbService.GetVacationListAsync(_loginService.User.Id);
                    var lastVacation = vacationDtos.LastOrDefault();
                    vacationInfo.Id = lastVacation.Id + 1;
                    await _vacationListOfflineDbService.AddUpdateFullVacationInfoAsync(vacationInfo);
                    //todo: add synchronization with database when user is connected to the internet
                }
            }
            catch (Exception e)
            {
                await _messageDialogService.ShowMessageDialogAsync(e.Message);
            }
        }

        public async Task<bool> DeleteVacationAsync(int id)
        {
            try
            {
                if (IsConnected)
                {
                    var isRemoved = await _vacationsWebService.DeleteVacationAsync(id);
                    if (isRemoved)
                    {
                        await _vacationListOfflineDbService.DeleteVacationAsync(id);
                    }
                    return true;
                }
                return await _vacationListOfflineDbService.DeleteVacationAsync(id);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
