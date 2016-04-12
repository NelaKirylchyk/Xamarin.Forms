using System.Collections.Generic;
using System.Linq;
using EpamVTSClient.DAL.Models.DTOModels;
using SQLite;
using VtsMockClient.Domain.Models;

namespace EpamVTSClient.DAL.Services.OfflineService
{
    public class VacationListOfflineDBService : IVacationListOfflineDBService
    {
        private readonly SQLiteConnection _connection;
        //public VacationListOfflineDBService(IConnectionManager connectionManager)
        //{
        //    _connection = connectionManager.GetSqLiteAsyncConnection();
        //}

        public VacationListOfflineDBService(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<VacationDTO> GetVacationList(int userId)
        {
            return _connection.Table<VacationDTO>().Where(r => r.EmployeeId == userId).ToList();
        }

        public void AddOrUpdateVacationList(int userId, IEnumerable<ShortVacationInfo> vacationList)
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

            _connection.Table<VacationDTO>().Delete(r => r.EmployeeId == userId);
            _connection.InsertAll(listOfVacationsDto);
        }
    }

    public interface IVacationListOfflineDBService
    {
        IEnumerable<VacationDTO> GetVacationList(int userId);
        void AddOrUpdateVacationList(int userId, IEnumerable<ShortVacationInfo> vacationList);
    }
}
