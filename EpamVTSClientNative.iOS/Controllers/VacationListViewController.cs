using System;
using System.Collections.Generic;
using System.Linq;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClientNative.iOS.Services;
using Microsoft.Practices.Unity;
using UIKit;

namespace EpamVTSClientNative.iOS.Controllers
{
    public class VacationListViewController : BaseViewController<VacationListViewModel>
    {
        private UITableView _table;

        protected override void Initialize()
        {
            base.Initialize();
            try
            {
                var vacationListViewModel = Factory.UnityContainer.Resolve<VacationListViewModel>();
                var vacationViewModels = vacationListViewModel.VacationList;
                ViewModel = vacationListViewModel;
                List<TableItem> tableItems = vacationViewModels.Select(vacationViewModel => new TableItem()
                {
                    Id = vacationViewModel.Id,
                    CellAccessory = UITableViewCellAccessory.DisclosureIndicator,
                    CellStyle = UITableViewCellStyle.Subtitle,
                    SubHeading = $"{vacationViewModel.StartDate} - {vacationViewModel.EndDate}",
                    Heading = $"{vacationViewModel.Type} - {vacationViewModel.VacationStatusToDisplay}"
                }).ToList();
                _table = new UITableView(View.Bounds)
                {
                    AutoresizingMask = UIViewAutoresizing.All,
                    Source = new TableSource(tableItems, this, NavigationService),
                    SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine,
                    RowHeight = 50,
                    Editing = true,
                    AllowsSelection = true,
                    AllowsSelectionDuringEditing = true
                }; // defaults to Plain style

                Add(_table);
            }
            catch (Exception e)
            {
                
            }
            

            //TableView.Editing = true;
        }

        //protected override void Initialize()
        //{
        //    base.Initialize();

        //    _table = new UITableView(View.Bounds) {AutoresizingMask = UIViewAutoresizing.All};
        //        // defaults to Plain style
        //    List<TableItem> tableItems = new List<TableItem>();
        //    ViewModel.LoadData.Execute(null);
        //    ObservableCollection<VacationViewModel> vacationViewModels = ViewModel.VacationList;
        //    foreach (var vacationViewModel in vacationViewModels)
        //    {
        //        var tableItem = new TableItem()
        //        {
        //            CellAccessory = UITableViewCellAccessory.DisclosureIndicator,
        //            CellStyle = UITableViewCellStyle.Subtitle,
        //            SubHeading = $"{vacationViewModel.StartDate} - {vacationViewModel.EndDate}",
        //            Heading = $"{vacationViewModel.Type} - {vacationViewModel.VacationStatusToDisplay}"
        //        };

        //        tableItems.Add(tableItem);
        //    }

        //    //View.Frame = new CoreGraphics.CGRect(0
        //    //            , UIApplication.SharedApplication.StatusBarFrame.Height
        //    //            , UIScreen.MainScreen.ApplicationFrame.Width
        //    //            , UIScreen.MainScreen.ApplicationFrame.Height);
        //    // credit for images and content
        //    // http://en.wikipedia.org/wiki/List_of_culinary_vegetables
        //    //tableItems.Add(new TableItem("Vegetables") { SubHeading = "65 items" });
        //    //tableItems.Add(new TableItem("Fruits") { SubHeading = "17 items" });
        //    //tableItems.Add(new TableItem("Flower Buds") { SubHeading = "5 items" });
        //    //tableItems.Add(new TableItem("Legumes") { SubHeading = "33 items" });
        //    //tableItems.Add(new TableItem("Bulbs") { SubHeading = "18 items" });
        //    //tableItems.Add(new TableItem("Tubers") { SubHeading = "43 items" });
        //    _tableSource = new TableSource(tableItems);

        //    _table.Source = _tableSource;

        //    //_done = new UIBarButtonItem(UIBarButtonSystemItem.Done, (s, e) => {
        //    //    _table.SetEditing(false, true);
        //    //    NavigationItem.RightBarButtonItem = _edit;
        //    //    _tableSource.DidFinishTableEditing(_table);
        //    //});
        //    //_edit = new UIBarButtonItem(UIBarButtonSystemItem.Edit, (s, e) => {
        //    //    if (_table.Editing)
        //    //        _table.SetEditing(false, true); // if we've half-swiped a row
        //    //    _tableSource.WillBeginTableEditing(_table);
        //    //    _table.SetEditing(true, true);
        //    //    NavigationItem.LeftBarButtonItem = null;
        //    //    NavigationItem.RightBarButtonItem = _done;
        //    //});

        //    //NavigationItem.RightBarButtonItem = _edit;



        //    Add(_table);
        //}
    }
}
