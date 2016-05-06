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
        UITableView _uiTableView;
        UILabel _title;

        protected override void Initialize()
        {
            base.Initialize();

            _title = ControlsExtensions.SetUiLabel(LocalizationService.Localize("vacationList"));
            TableSource tableSource;
            List<TableItem> tableItems = GetTableItems(out tableSource);
            _uiTableView = ControlsExtensions.SetUiTableView(tableItems, tableSource, View.Bounds);

            Add(_title);
            Add(_uiTableView);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.InsertSubview(new UIImageView(UIImage.FromBundle("illustration")), 0);

            AddConstraints();
        }

        private void AddConstraints()
        {
            const int margin = 20;
            View.AddConstraints(
                _title.AtTopOf(View, margin),
                _title.CenterX().EqualTo().CenterXOf(View),

                _uiTableView.Below(_title, margin),
                _uiTableView.Width().EqualTo().WidthOf(View),
                _uiTableView.Height().EqualTo().HeightOf(View)
                );
        }

        private List<TableItem> GetTableItems(out TableSource tableSource)
        {
            List<TableItem> tableItems = ViewModel.VacationList.Select(vacationViewModel => new TableItem()
            {
                Id = vacationViewModel.Id,
                CellAccessory = UITableViewCellAccessory.DisclosureIndicator,
                CellStyle = UITableViewCellStyle.Subtitle,
                SubHeading = $"{vacationViewModel.StartDate.ToString("d")} - {vacationViewModel.EndDate.ToString("d")}",
                Heading = $"{vacationViewModel.Type} - {vacationViewModel.VacationStatusToDisplay}"
            }).ToList();

            tableSource = new TableSource(tableItems, this, NavigationService);
            return tableItems;
        }
    }
}
