using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Converters;
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