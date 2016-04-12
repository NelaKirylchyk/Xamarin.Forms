using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EpamVTSClient.BLL.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public string Title { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
