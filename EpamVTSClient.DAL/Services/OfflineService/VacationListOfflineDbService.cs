using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpamVTSClient.DAL.Models;
using EpamVTSClient.DAL.Models.DTOModels;
using SQLite;

namespace EpamVTSClient.DAL.Services.OfflineService
{
    public class VacationListOfflineDBService : IVacationListOfflineDBService
    {
        private readonly SQLiteAsyncConnection _connection;

        public VacationListOfflineDBService(SQLiteAsyncConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<VacationDTO>> GetVacationList(int userId)
        {
            return await _connection.Table<VacationDTO>().Where(r => r.EmployeeId == userId).ToListAsync();
        }

        public async Task AddOrUpdateVacationList(int userId, IEnumerable<ShortVacationInfo> vacationList)
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

            var vacationListToBeRemoved = await _connection.Table<VacationDTO>().Where(r => r.EmployeeId == userId).ToListAsync();
            foreach (VacationDTO vacation in vacationListToBeRemoved)
            {
                await _connection.DeleteAsync(vacation);
            }
            await _connection.InsertAllAsync(listOfVacationsDto);
        }
    }

    public interface IVacationListOfflineDBService
    {
        Task<List<VacationDTO>> GetVacationList(int userId);
        Task AddOrUpdateVacationList(int userId, IEnumerable<ShortVacationInfo> vacationList);
    }
}
