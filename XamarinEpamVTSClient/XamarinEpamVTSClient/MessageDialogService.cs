using System.Threading.Tasks;
using EpamVTSClient.Core.Services;
using Application = Xamarin.Forms.Application;

namespace XamarinEpamVTSClient
{
    public class MessageDialogService : IMessageDialogService
    {
        public async Task ShowMessageDialogAsync(string message)
        {
            await Application.Current.MainPage.DisplayAlert("Error", message, "OK");
        }
    }
}