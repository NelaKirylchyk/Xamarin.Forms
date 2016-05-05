using System.Drawing;
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
                TextColor = UIColor.Blue,
                Text = LocalizationService.Localize("menu")
            };

            var introButton = new UIButton(UIButtonType.System) {Frame = new RectangleF(0, 180, 260, 20)};
            introButton.SetTitle(LocalizationService.Localize("vacationList"), UIControlState.Normal);
            introButton.TouchUpInside += (sender, e) =>
            {
                SidebarController.ChangeContentView(new VacationListViewController());
                SidebarController.CloseMenu();
            };

            var contentButton = new UIButton(UIButtonType.System) {Frame = new RectangleF(0, 220, 260, 20)};
            contentButton.SetTitle(LocalizationService.Localize("VacationAddVacTitle"), UIControlState.Normal);
            contentButton.TouchUpInside += (sender, e) =>
            {
                var vacationViewController = new VacationViewController();
                SidebarController.ChangeContentView(vacationViewController);
                SidebarController.CloseMenu();
            };

            View.Add(title);
            View.Add(introButton);
            View.Add(contentButton);
        }
    }

    public class MenuViewModel : ViewModelBase
    {
    }
}
