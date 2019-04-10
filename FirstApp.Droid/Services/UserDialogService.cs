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
         
            IMvxAndroidCurrentTopActivity top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new AlertDialog.Builder(act);
            adb.SetTitle(title);
            adb.SetMessage(message);
     
            adb.SetPositiveButton(okbtnText, (sender, args) => { /* some logic */ });
            adb.Create().Show();
        }

        public void ChoosePhoto(string message, string title, string okbtnText, string escbtnText)
        {
            var top = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new AlertDialog.Builder(act);
            adb.SetTitle(title);
            adb.SetMessage(message);
            
            adb.SetPositiveButton(okbtnText, (sender, EventArgs ) => { });
            adb.SetNegativeButton(escbtnText, (sender, args) => { /* some logic */ });
            adb.Create().Show();
        }
    }


}
            
    




