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
    public class ByteArrayToImgValueConverter: MvxValueConverter<byte[], UIImage>
    {
        protected override UIImage Convert(byte[] value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = NSData.FromArray(value);
            var uiimage = UIImage.LoadFromData(data);

            return uiimage;
        }
        

    }
}