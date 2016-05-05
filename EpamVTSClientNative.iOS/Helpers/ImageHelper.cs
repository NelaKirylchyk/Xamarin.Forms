using System;
using Foundation;
using UIKit;

namespace EpamVTSClientNative.iOS.Helpers
{
    public static class ImageHelper
    {
        public static UIImage Base64ToImage(string base64)
        {
            byte[] encodedDataAsBytes = Convert.FromBase64String(base64);
            NSData imageData = NSData.FromArray(encodedDataAsBytes);
            var img = UIImage.LoadFromData(imageData);
            return img;
        }
    }
}
