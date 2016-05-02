using System.Threading.Tasks;
using EpamVTSClient.Core.Services;

namespace EpamVTSClientNative.iOS.Services
{
    public class MessageDialogService : IMessageDialogService
    {
        public Task ShowMessageDialogAsync(string message)
        {
            return Task.FromResult(true);
        }
    }
}