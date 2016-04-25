using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpamVTSClient.Core.Services;
using EpamVTSClient.DAL.Models;
using EpamVTSClient.DAL.Models.DTOModels;
using SQLite;

namespace EpamVTSClient.DAL.Services.OfflineService
{
    public class VacationListOfflineDBService : IVacationListOfflineDBService
    {
        private readonly SQLiteAsyncConnection _connection;
        private readonly IMessageDialogService _messageDialogService;

        public VacationListOfflineDBService(SQLiteAsyncConnection connection, IMessageDialogService messageDialogService)
        {
            _connection = connection;
            _messageDialogService = messageDialogService;
        }

        public async Task<List<VacationDTO>> GetVacationListAsync(int userId)
        {
            try
            {
                return await _connection.Table<VacationDTO>().Where(r => r.EmployeeId == userId).ToListAsync();
            }
            catch (Exception e)
            {
                await _messageDialogService.ShowMessageDialogAsync(e.Message);
            }
            return null;
        }

        public async Task AddOrUpdateVacationListAsync(int userId, IEnumerable<ShortVacationInfo> vacationList)
        {
            try
            {
                var shortVacationInfos = vacationList as IList<ShortVacationInfo> ?? vacationList.ToList();
                List<VacationDTO> listOfVacationsDto = new List<VacationDTO>(shortVacationInfos.Count);
                listOfVacationsDto.AddRange(shortVacationInfos
                    .Select(shortVacationInfo => new VacationDTO
                    {
                        Id = shortVacationInfo.Id,
                        Type = shortVacationInfo.Type,
                        Status = shortVacationInfo.Status,
                        StartDate = shortVacationInfo.StartDate,
                        EndDate = shortVacationInfo.EndDate,
                        EmployeeId = userId
                    }));

                List<VacationDTO> vacationListToBeRemoved = await _connection.Table<VacationDTO>().Where(r => r.EmployeeId == userId).ToListAsync();
                foreach (VacationDTO vacation in vacationListToBeRemoved)
                {
                    await _connection.DeleteAsync(vacation).ConfigureAwait(false);
                }
                await _connection.InsertAllAsync(listOfVacationsDto);
            }
            catch (Exception e)
            {
                await _messageDialogService.ShowMessageDialogAsync(e.Message);
            }
        }

        public async Task<FullVacationDTO> GetFullVacationAsync(int vacationId)
        {
            try
            {
                return await _connection.Table<FullVacationDTO>().Where(r => r.Id == vacationId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                await _messageDialogService.ShowMessageDialogAsync(e.Message);
            }
            return null;
        }

        public async Task AddUpdateFullVacationInfoAsync(VacationInfo vacationInfo)
        {
            try
            {
                FullVacationDTO oldFullVacationDto = await _connection.Table<FullVacationDTO>().Where(r => r.Id == vacationInfo.Id).FirstOrDefaultAsync();

                if (oldFullVacationDto != null)
                {
                    await _connection.DeleteAsync(oldFullVacationDto);
                }
                FullVacationDTO newFullVacationDto = new FullVacationDTO();
                newFullVacationDto.Update(vacationInfo);
                await _connection.InsertAsync(newFullVacationDto);
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
                var vacationToBeRemoved = await _connection.Table<VacationDTO>().Where(r => r.Id == id).FirstOrDefaultAsync();
                if (vacationToBeRemoved != null)
                {
                    await _connection.DeleteAsync(vacationToBeRemoved);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
