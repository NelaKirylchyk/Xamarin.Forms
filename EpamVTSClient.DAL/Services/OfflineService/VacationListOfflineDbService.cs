using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpamVTSClient.DAL.Models;
using EpamVTSClient.DAL.Models.DTOModels;
using SQLite;
using Xamarin.Forms;

namespace EpamVTSClient.DAL.Services.OfflineService
{
    public class VacationListOfflineDBService : IVacationListOfflineDBService
    {
        private readonly SQLiteAsyncConnection _connection;

        public VacationListOfflineDBService(SQLiteAsyncConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<VacationDTO>> GetVacationListAsync(int userId)
        {
            try
            {
                return await _connection.Table<VacationDTO>().Where(r => r.EmployeeId == userId).ToListAsync();
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
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
                    await _connection.DeleteAsync(vacation);
                }
                await _connection.InsertAllAsync(listOfVacationsDto);
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
        }
    }
}
