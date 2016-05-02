using System;
using System.Collections.Generic;
using System.Text;
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
                TextColor = UIColor.White
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
    }
}
