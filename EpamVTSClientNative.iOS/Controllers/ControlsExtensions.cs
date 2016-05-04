using System;
using Foundation;
using UIKit;

namespace EpamVTSClientNative.iOS.Controllers
{
    public static class ControlsExtensions
    {
        public static UILabel SetUiLabel(string text)
        {
            return new UILabel()
            {
                Text = text,
                TextColor = UIColor.White,
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
        }

        public static UITextField SetTextField(string placeholder)
        {
            return new UITextField()
            {
                Placeholder = placeholder,
                TextColor = UIColor.White,
                BorderStyle = UITextBorderStyle.RoundedRect,
                TranslatesAutoresizingMaskIntoConstraints = false,
                BackgroundColor = UIColor.LightGray
            };
        }

        public static UITextField SetSecureTextField(string placeholder)
        {
            var uiTextField = SetTextField(placeholder);
            uiTextField.SecureTextEntry = true;
            return uiTextField;
        }

        public static UIButton SetButton(string title)
        {
            var button = UIButton.FromType(UIButtonType.RoundedRect);
            button.BackgroundColor = UIColor.Gray;
            button.SetTitleColor(UIColor.White, UIControlState.Normal);
            button.SetTitle(title, UIControlState.Normal);

            return button;
        }

        public static UIDatePicker SetDatePicker(DateTime endDate)
        {
            var uiDatePicker = new UIDatePicker
            {
                Mode = UIDatePickerMode.Date,
                AutoresizingMask =
                    UIViewAutoresizing.FlexibleBottomMargin | UIViewAutoresizing.FlexibleHeight |
                    UIViewAutoresizing.FlexibleWidth,
                Date = NSDate.Now,
                //| UIViewAutoresizing.FlexibleRightMargin
               
            };
            return uiDatePicker;
        }

        public static UIPickerView SetUiPicker(UIPickerViewModel pickerDataModel)
        {
            return new UIPickerView
            {
                Model = pickerDataModel,
                ShowSelectionIndicator = false
            };
        }
    }
}
