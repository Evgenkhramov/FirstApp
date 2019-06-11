using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FirstApp.Core;
using FirstApp.Core.ViewModels;
using FirstApp.Droid.Interfaces;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame_new, false)]
    [Register("firstApp.Droid.Views.UserDataFragment")]
    public class UserDataFragment : BaseFragment<MainView, UserDataViewModel>, IBackButtonListener
    {
        #region Variables

        private static readonly int PickImageId = 1000;
        private Button _menuButton;
        private Button _btnCamera;
        private ImageView _cameraPreview;

        protected override int FragmentId => Resource.Layout.UserDataFragment;

        #endregion Variables

        #region Methods

        private async void GetPermissions(object sender, EventArgs e)
        {

            if (await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage) != PermissionStatus.Granted ||
                      await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera) != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage, Permission.Camera);

                return;
            }

            ChoosePhoto();
        }

        public void SelectPhoto(string message, string title, string okbtnText, string escbtnText)
        {
            var UserChoose = new AlertDialog.Builder(Context);

            UserChoose.SetTitle(title);
            UserChoose.SetMessage(message);
            UserChoose.SetPositiveButton(okbtnText, (sender, EventArgs) => { ViewModel.ChoosePictureCommand.Execute(null); });
            UserChoose.SetNegativeButton(escbtnText, (sender, EventArgs) => { ViewModel.TakePictureCommand.Execute(null); });

            UserChoose.Create().Show();
        }

        public void HandleBackPressed()
        {
            ViewModel.CloseFragment.Execute();
        }

        public void ChoosePhoto()
        {
            SelectPhoto(Constants.SelectPhoto, Constants.PleaseSelectPhoto, Constants.FromMemory, Constants.FromCamera);
        }

        private void OpenAndroidMenu(object sender, EventArgs e)
        {
            OpenMenu();
        }

        #endregion Methods

        #region Overrides

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _menuButton = view.FindViewById<Button>(Resource.Id.menu_icon);
            _btnCamera = view.FindViewById<Button>(Resource.Id.btnCamera);
            _cameraPreview = view.FindViewById<ImageView>(Resource.Id.camera_preview);
            _btnCamera.Click += GetPermissions;
            _menuButton.Click += OpenAndroidMenu;

            return view;
        }

        public override void OnDestroyView()
        {
            _btnCamera.Click -= GetPermissions;
            _menuButton.Click -= OpenAndroidMenu;

            base.OnDestroyView();
        }

        #endregion Overrides
    }
}