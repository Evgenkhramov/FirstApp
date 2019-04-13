using FirstApp.Core.Interfaces;
using Android.App;
using MvvmCross;
using MvvmCross.Platforms.Android;
using MvvmCross.Commands;
using System.Threading.Tasks;

namespace FirstApp.Droid.Services
{
    public class UserDialogService : IUserDialogService
    {

        public void ShowAlertForUser(string title, string message, string okbtnText)
        {

            IMvxAndroidCurrentTopActivity top = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new AlertDialog.Builder(act);
            adb.SetTitle(title);
            adb.SetMessage(message);

            adb.SetPositiveButton(okbtnText, (sender, args) => { /* some logic */ });
            adb.Create().Show();
        }

        public Task<bool> ShowAlertForUserWithSomeLogic(string title, string message, string okbtnText, string nobtnText)
        {
            var tcs = new TaskCompletionSource<bool>();         
            IMvxAndroidCurrentTopActivity top = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;
            var adb = new AlertDialog.Builder(act);
            adb.SetTitle(title);
            adb.SetMessage(message);
            adb.SetNegativeButton(nobtnText, (sender, args) => { tcs.TrySetResult(false); });
            adb.SetPositiveButton(okbtnText, (sender, args) => { tcs.TrySetResult(true); });
            adb.Create().Show();
            return tcs.Task;
        }

        public void ChoosePhoto(string message, string title, string okbtnText, string escbtnText)
        {
            var top = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new AlertDialog.Builder(act);
            adb.SetTitle(title);
            adb.SetMessage(message);

            adb.SetPositiveButton(okbtnText, (sender, EventArgs) => { });
            adb.SetNegativeButton(escbtnText, (sender, args) => { /* some logic */ });
            adb.Create().Show();
        }
    }


}






