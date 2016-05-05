using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels;
using Foundation;
using UIKit;

namespace EpamVTSClientNative.iOS.Controllers.Table
{
    public class TableSource : UITableViewSource
    {
        List<TableItem> _tableItems;
        string cellIdentifier = "TableCell";
        readonly VacationListViewController owner;
        private INavigationService _navigationService;

        public TableSource(List<TableItem> items, VacationListViewController owner, INavigationService navigationService)
        {
            _tableItems = items;
            this.owner = owner;
            _navigationService = navigationService;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _tableItems.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //UIAlertController okAlertController = UIAlertController.Create("Row Selected", _tableItems[indexPath.Row].Heading, UIAlertControllerStyle.Alert);
            //okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            //owner.PresentViewController(okAlertController, true, null);

            TableItem tableItem = _tableItems[indexPath.Row];
            _navigationService.NavigateToAsync<EditVacationViewModel>(tableItem.Id.ToString());

            tableView.DeselectRow(indexPath, true);
            owner.DismissModalViewController(true);
        }

        public override void AccessoryButtonTapped(UITableView tableView, NSIndexPath indexPath)
        {
            base.AccessoryButtonTapped(tableView, indexPath);
            TableItem tableItem = _tableItems[indexPath.Row];
            _navigationService.NavigateToAsync<VacationViewModel>(tableItem.Id.ToString());

            tableView.DeselectRow(indexPath, true);
            owner.DismissModalViewController(true);
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
            var cellStyle = UITableViewCellStyle.Subtitle;
            if (cell == null)
            {
                cell = new UITableViewCell(cellStyle, cellIdentifier);
            }

            cell.TextLabel.Text = _tableItems[indexPath.Row].Heading;

            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            if (cellStyle == UITableViewCellStyle.Subtitle
               || cellStyle == UITableViewCellStyle.Value1
               || cellStyle == UITableViewCellStyle.Value2)
            {
                cell.DetailTextLabel.Text = _tableItems[indexPath.Row].SubHeading;
            }

            if (cellStyle != UITableViewCellStyle.Value2)
                cell.ImageView.Image = UIImage.FromBundle("rainbow");

            return cell;
        }
        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            if (editingStyle == UITableViewCellEditingStyle.Delete)
            {
                TableItem tableItem = _tableItems[indexPath.Row];
                var vacationViewModel = owner.ViewModel.VacationList.FirstOrDefault(r => r.Id == tableItem.Id);
                if (vacationViewModel != null)
                {
                    _tableItems.RemoveAt(indexPath.Row);
                    tableView.DeleteRows(new[] { indexPath }, UITableViewRowAnimation.Fade);
                    
                    vacationViewModel.DeleteVacationCommand.Execute(null);
                }
            }
        }

        public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
        {
            return "Remove";
        }

        public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return UITableViewCellEditingStyle.Delete;
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true; // return false if you wish to disable editing for a specific indexPath or for all rows
        }
    }
}
