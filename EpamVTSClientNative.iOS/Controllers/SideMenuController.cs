using System.Drawing;
using Cirrious.FluentLayouts.Touch;
using EpamVTSClient.BLL.ViewModels.Base;
using UIKit;

namespace EpamVTSClientNative.iOS.Controllers
{
    public class SideMenuController : BaseViewController<MenuViewModel>
    {
        protected override void Initialize()
        {
            base.Initialize();
            View.BackgroundColor = UIColor.FromRGB(.9f, .9f, .9f);

            var title = new UILabel(new RectangleF(0, 50, 270, 20))
            {
                Font = UIFont.SystemFontOfSize(24.0f),
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.White,
                Text = LocalizationService.Localize("menu")
            };

            var vacationListButton = new UIButton(UIButtonType.System) { Frame = new RectangleF(0, 180, 260, 20) };
            vacationListButton.SetTitle(LocalizationService.Localize("vacationList"), UIControlState.Normal);
            vacationListButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            vacationListButton.TouchUpInside += (sender, e) =>
            {
                SidebarController.ChangeContentView(new VacationListViewController());
                SidebarController.CloseMenu();
            };

            var addVacationButton = new UIButton(UIButtonType.System) { Frame = new RectangleF(0, 220, 260, 20) };
            addVacationButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            addVacationButton.SetTitle(LocalizationService.Localize("VacationAddVacTitle"), UIControlState.Normal);
            addVacationButton.TouchUpInside += (sender, e) =>
            {
                var vacationViewController = new VacationViewController();
                SidebarController.ChangeContentView(vacationViewController);
                SidebarController.CloseMenu();
            };

            View.Add(title);
            View.Add(vacationListButton);
            View.Add(addVacationButton);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.BackgroundColor = UIColor.LightGray;

            const int margin = 20;
            View.AddConstraints(
                title.AtTopOf(View, margin),
                title.AtLeftOf(View, margin),

                vacationListButton.Below(title, 50),
                vacationListButton.AtLeftOf(View, margin),

                addVacationButton.Below(vacationListButton, margin),
                addVacationButton.AtLeftOf(View, margin));
        }
    }

    public class MenuViewModel : ViewModelBase
    {
    }
}
