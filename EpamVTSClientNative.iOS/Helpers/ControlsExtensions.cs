using System;
using System.Collections.Generic;
using CoreGraphics;
using EpamVTSClientNative.iOS.Controllers.Table;
using UIKit;

namespace EpamVTSClientNative.iOS.Helpers
{
    public static class ControlsExtensions
    {
        public static UILabel SetUiLabel(string text)
        {
            return new UILabel()
            {
                Text = text,
                MinimumFontSize = 17,
                TextColor = UIColor.White,
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
        }

        public static UILabel SetErrorUiLabel()
        {
            return new UILabel()
            {
                Enabled = false,
                MinimumFontSize = 2,
                TextColor = UIColor.Red
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
            button.BackgroundColor = UIColor.LightGray;
            button.Font = UIFont.PreferredBody;
            button.SetTitleColor(UIColor.White, UIControlState.Normal);
            button.SetTitle(title, UIControlState.Normal);

            return button;
        }

        public static UIDatePicker SetDatePicker(DateTime datetime)
        {
            var uiDatePicker = new UIDatePicker
            {
                Mode = UIDatePickerMode.Date,
                AutoresizingMask = UIViewAutoresizing.FlexibleBottomMargin | UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth,
                Date = datetime.ConvertDateTimeToNsDate()
            };
            return uiDatePicker;
        }

        public static UIPickerView SetUiPicker(UIPickerViewModel pickerDataModel, nint index)
        {
            var uiPickerView = new UIPickerView
            {
                Model = pickerDataModel,
                ShowSelectionIndicator = false,
                TintColor = UIColor.White
        };
            uiPickerView.Select(index, 0, true);
            return uiPickerView;
        }


        public static UITableView SetUiTableView(List<TableItem> tableItems, TableSource tableSource, CGRect сgRect)
        {
            return new UITableView()
            {
                AutoresizingMask = UIViewAutoresizing.None,
                Source = tableSource,
                SeparatorStyle = UITableViewCellSeparatorStyle.None,
                RowHeight = 50,
                Editing = true,
                BackgroundColor = UIColor.Clear,
                AllowsSelection = true,
                AllowsSelectionDuringEditing = true
            };
        }
    }
}
