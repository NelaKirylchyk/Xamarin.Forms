using System.Threading.Tasks;

namespace EpamVTSClient.Core.Services
{
    public interface IMessageDialogService
    {
        Task ShowMessageDialogAsync(string message);
    }
}
