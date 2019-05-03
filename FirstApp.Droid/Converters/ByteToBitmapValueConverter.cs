using Android.Graphics;
using Android.Util;
using MvvmCross.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FirstApp.Droid.Converters
{
    public class StringToBitmapValueConverter : MvxValueConverter<string, Bitmap>
    {
        protected override Bitmap Convert(string imageString, Type targetType, object parameter, CultureInfo culture)
        {
            if (imageString.Length!= 0)
            {
                byte[] imageAsBytes = Base64.Decode(imageString, Base64Flags.Default);
                return BitmapFactory.DecodeByteArray(imageAsBytes, 0, imageAsBytes.Length);
            }
            return null;
        }      
    }
}
