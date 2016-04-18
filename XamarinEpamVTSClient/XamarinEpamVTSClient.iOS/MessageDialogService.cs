using System.Threading.Tasks;
using EpamVTSClient.Core.Services;

namespace XamarinEpamVTSClient.iOS
{
    public class MessageDialogService : IMessageDialogService
    {
        public async Task ShowMessageDialogAsync(string message)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", message, "OK");
        }
    }
}