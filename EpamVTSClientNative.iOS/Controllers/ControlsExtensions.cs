using System;
using CoreAnimation;
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

        public static UIDatePicker SetDatePicker(DateTime datetime)
        {
            var uiDatePicker = new UIDatePicker
            {
                Mode = UIDatePickerMode.Date,
                AutoresizingMask =
                    UIViewAutoresizing.FlexibleBottomMargin | UIViewAutoresizing.FlexibleHeight |
                    UIViewAutoresizing.FlexibleWidth,
                Date = ConvertDateTimeToNSDate(datetime)
                //| UIViewAutoresizing.FlexibleRightMargin

            };
            return uiDatePicker;
        }

        public static NSDate ConvertDateTimeToNSDate(DateTime date)
        {
            DateTime newDate = TimeZone.CurrentTimeZone.ToLocalTime(
                new DateTime(2001, 1, 1, 0, 0, 0));
            return NSDate.FromTimeIntervalSinceReferenceDate(
                (date - newDate).TotalSeconds);
        }

        public static UIPickerView SetUiPicker(UIPickerViewModel pickerDataModel, nint index)
        {
            var uiPickerView = new UIPickerView
            {
                Model = pickerDataModel,
                ShowSelectionIndicator = false,
            };
            uiPickerView.Select(index, 0, true);
            return uiPickerView;
        }
    }
}
