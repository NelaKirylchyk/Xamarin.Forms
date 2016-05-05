using System.Collections.Generic;
using System.Linq;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClientNative.iOS.Controllers.Table;
using EpamVTSClientNative.iOS.Helpers;
using UIKit;

namespace EpamVTSClientNative.iOS.Controllers
{
    public class VacationListViewController : BaseViewController<VacationListViewModel>
    {
        protected override void Initialize()
        {
            base.Initialize();

            SidebarController.Disabled = false;

            List<TableItem> tableItems = ViewModel.VacationList.Select(vacationViewModel => new TableItem()
            {
                Id = vacationViewModel.Id,
                CellAccessory = UITableViewCellAccessory.DisclosureIndicator,
                CellStyle = UITableViewCellStyle.Subtitle,
                SubHeading = $"{vacationViewModel.StartDate} - {vacationViewModel.EndDate}",
                Heading = $"{vacationViewModel.Type} - {vacationViewModel.VacationStatusToDisplay}"
            }).ToList();

            var tableSource = new TableSource(tableItems, this, NavigationService);
            var uiTableView = ControlsExtensions.SetUiTableView(tableItems, tableSource, View.Bounds);
            Add(uiTableView);
        }
    }
}
