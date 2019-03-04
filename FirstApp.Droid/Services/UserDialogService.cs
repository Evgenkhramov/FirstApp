using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirstApp.Core.Interfaces;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FirstApp.Droid.Views;
using MvvmCross;
using MvvmCross.Platforms.Android;

namespace FirstApp.Droid.Services
{
    public class UserDialogService : IUserDialogService
    {
        public void ShowAlertForUser(string title, string message, string okbtnText)
        {
            // <summary>Alerts the user with a simple OK dialog and provides a <paramref name="message"/>.</summary>
            // <param name="message">The message.</param>
            // <param name="title">The title.</param>
            // <param name="okbtnText">The okbtn text.</param>

            IMvxAndroidCurrentTopActivity top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new AlertDialog.Builder(act);
            adb.SetTitle(title);
            adb.SetMessage(message);
            //adb.SetIcon(Resource.Drawable.Icon);
            adb.SetPositiveButton(okbtnText, (sender, args) => { /* some logic */ });
            adb.Create().Show();
        }
    }


}
            
    




