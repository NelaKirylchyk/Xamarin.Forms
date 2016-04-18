using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamVTSClient.Core.Services
{
    public interface IMessageDialogService
    {
        Task ShowMessageDialogAsync(string message);
    }
}
