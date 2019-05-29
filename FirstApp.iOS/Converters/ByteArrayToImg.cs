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
            var imageBytes = System.Convert.FromBase64String(value);
            var imageData = NSData.FromArray(imageBytes);
            var uiImage = UIImage.LoadFromData(imageData);

            return uiImage;
        }
    }
}