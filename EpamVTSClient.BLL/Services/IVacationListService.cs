using System.Collections.Generic;
using System.Threading.Tasks;
using EpamVTSClient.DAL.Models;

namespace EpamVTSClient.BLL.Services
{
    public interface IVacationListService
    {
        Task<IEnumerable<ShortVacationInfo>> GetVacationsAsync();
        Task<VacationInfo> GetFullVacationInfoAsync(int vacationId);

        Task AddUpdateVacationInfoAsync(VacationInfo vacationInfo);

        Task<bool> DeleteVacationAsync(int id);
    }
}