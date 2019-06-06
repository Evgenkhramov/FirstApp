using Foundation;
using MvvmCross.Converters;
using System;
using System.Globalization;
using UIKit;

namespace FirstApp.iOS.Converters
{
    public class ByteArrayToImgValueConverter: MvxValueConverter<string, UIImage>
    {
        protected override UIImage Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] imageBytes = System.Convert.FromBase64String(value);
            NSData imageData = NSData.FromArray(imageBytes);
            UIImage uiImage = UIImage.LoadFromData(imageData);

            return uiImage;
        }
    }
}