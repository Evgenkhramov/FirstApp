using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FirstApp.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using FirstApp.Droid.Interfaces;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame_new, false)]
    [Register("firstApp.Droid.Views.UserDataFragment")]
    public class UserData : BaseFragment<UserDataViewModel>, IBackButtonListener
    {
        public static readonly int PickImageId = 1000;
        Button MenuButton;
        Button BtnCamera;
        ImageView CameraPreview;
        private string _imagePath;
        protected override int FragmentId => Resource.Layout.UserDataFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);
            MenuButton = view.FindViewById<Button>(Resource.Id.menu_icon);
            BtnCamera = view.FindViewById<Button>(Resource.Id.btnCamera);
            CameraPreview = view.FindViewById<ImageView>(Resource.Id.camera_preview);
            BtnCamera.Click += GetPermissions;
            MenuButton.Click += (object sender, EventArgs e) =>
            {
                OpenMenu();
            };
            return view;
        }

        public async void GetPermissions(object sender, System.EventArgs e)
        {

            if (await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage) != PermissionStatus.Granted ||
                      await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Camera) != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.Storage, Plugin.Permissions.Abstractions.Permission.Camera);                  
            }

            if (await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage) == PermissionStatus.Granted &&
                     await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Camera) == PermissionStatus.Granted)
            {
                ChoosePhoto();
            }
        }

        public void SelectPhoto(string message, string title, string okbtnText, string escbtnText)
        {
            var adb = new AlertDialog.Builder(Context);
            adb.SetTitle(title);
            adb.SetMessage(message);
            adb.SetPositiveButton(okbtnText, (sender, EventArgs) => { ViewModel.ChoosePictureCommand.Execute(null); });
            adb.SetNegativeButton(escbtnText, (sender, EventArgs) => { ViewModel.TakePictureCommand.Execute(null); });
            adb.Create().Show();
        }

        public void OnBackPressed()
        {
            ViewModel.CloseFragment.Execute();
        }

        public void ChoosePhoto()
        {
            SelectPhoto("Select Photo", "Please, select photo.", "From memory", "From camera");
        }
    }
}