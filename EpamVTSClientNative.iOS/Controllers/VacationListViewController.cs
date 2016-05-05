using System.Collections.Generic;
using System.Linq;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClientNative.iOS.Controllers.Table;
using EpamVTSClientNative.iOS.Helpers;
using Cirrious.FluentLayouts.Touch;
using UIKit;

namespace EpamVTSClientNative.iOS.Controllers
{
    public class VacationListViewController : BaseViewController<VacationListViewModel>
    {
        protected override void Initialize()
        {
            base.Initialize();

            var title = ControlsExtensions.SetUiLabel(LocalizationService.Localize("vacationList"));
            Add(title);

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


            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.InsertSubview(new UIImageView(UIImage.FromBundle("illustration")), 0);

            View.AddConstraints(
                title.AtTopOf(View, 20),
                title.CenterX().EqualTo().CenterXOf(View),
                uiTableView.Below(title, 20),
                uiTableView.Width().EqualTo().WidthOf(View),
                uiTableView.Height().EqualTo().HeightOf(View)
                );
        }
    }
}
