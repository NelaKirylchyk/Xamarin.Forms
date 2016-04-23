using System.Collections.Generic;
using System.Threading.Tasks;
using EpamVTSClient.DAL.Models;
using EpamVTSClient.DAL.Models.DTOModels;

namespace EpamVTSClient.DAL.Services.OfflineService
{
    public interface IVacationListOfflineDBService
    {
        Task<List<VacationDTO>> GetVacationListAsync(int userId);
        Task AddOrUpdateVacationListAsync(int userId, IEnumerable<ShortVacationInfo> vacationList);
        Task<FullVacationDTO> GetFullVacationAsync(int vacationId);
        Task AddUpdateFullVacationInfoAsync(VacationInfo vacationInfo);
        Task<bool> DeleteVacationAsync(int id);
    }
}