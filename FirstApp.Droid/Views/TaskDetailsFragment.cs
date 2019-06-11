using Acr.UserDialogs.Infrastructure;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using FirstApp.Core;
using FirstApp.Core.ViewModels;
using FirstApp.Droid.Interfaces;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame_new, true)]
    [Register("firstApp.Droid.Views.TaskDetailsFragment")]
    public class TaskDetailsFragment : BaseFragment<MainView, TaskDetailsViewModel>, IBackButtonListener
    {
        #region Variables

        static readonly int READ_EXTERNAL_STORAGE = 0;
        static readonly int ACCESS_COARSE_LOCATION = 1;

        private readonly int _fileCode = 1000;
        private readonly int _mapCode = 1100;
        private Button _getFileButton;
        private Button _getMarkerButton;
        private readonly Button _menuButton;

        protected override int FragmentId => Resource.Layout.TaskDetailsFragment;

        #endregion Variables

        #region Properties

        public string TAG { get; private set; }

        #endregion Properties

        #region Methods

        public void GetMapPositionPermissions(object sender, EventArgs e)
        {
            if (ContextCompat.CheckSelfPermission(Activity, Manifest.Permission.AccessCoarseLocation) != (int)Permission.Granted)
            {
                RequestPermissions(new string[] { Manifest.Permission.AccessCoarseLocation }, ACCESS_COARSE_LOCATION);

                return;
            }

            AddMarker();
        }

        public void GetStoragePermissions(object sender, System.EventArgs e)
        {
            if (ContextCompat.CheckSelfPermission(Activity, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted)
            {
                RequestPermissions(new string[] { Manifest.Permission.ReadExternalStorage }, READ_EXTERNAL_STORAGE);

                return;
            }

            OpenFile();
        }

        public void AddMarker()
        {
            ViewModel.AddMarkerCommand.Execute();
        }

        public void OpenFile()
        {
            var intent = new Intent();

            intent.SetType(Constants.IntentType);
            intent.SetAction(Intent.ActionGetContent);

            StartActivityForResult(Intent.CreateChooser(intent, Constants.SelectFile), _fileCode);
        }

        public void HandleBackPressed()
        {
            ViewModel.BackViewCommand.Execute();
        }

        #endregion Methods

        #region Overrides

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _getMarkerButton = view.FindViewById<Button>(Resource.Id.getMapMarker);
            _getMarkerButton.Click += GetMapPositionPermissions;

            _getFileButton = view.FindViewById<Button>(Resource.Id.getFileButton);
            _getFileButton.Click += GetStoragePermissions;

            return view;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == READ_EXTERNAL_STORAGE && grantResults[default(int)] == Permission.Granted)
            {
                OpenFile();

                return;
            }

            if (requestCode == READ_EXTERNAL_STORAGE && grantResults[default(int)] != Permission.Granted)
            {
                Log.Info(TAG, Constants.LocalStoragePermissionNotGranted);

                return;
            }

            if (requestCode == ACCESS_COARSE_LOCATION && grantResults[default(int)] == Permission.Granted)
            {
                AddMarker();

                return;
            }
            if (requestCode == ACCESS_COARSE_LOCATION && grantResults[default(int)] != Permission.Granted)
            {
                Log.Info(TAG, Constants.CoarsePermissionNotGranted);

                return;
            }

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            string fileName = null;

            if (requestCode == _fileCode && resultCode == (int)Result.Ok)
            {
                Uri uri = new Uri(data.DataString);

                fileName = System.IO.Path.GetFileNameWithoutExtension(uri.LocalPath);

                ViewModel.SaveFileName(fileName);
            }
        }

        public override void OnResume()
        {
            base.OnResume();
        }

        public override void OnDestroyView()
        {
            _getMarkerButton.Click -= GetMapPositionPermissions;
            _getFileButton.Click -= GetStoragePermissions;

            base.OnDestroyView();
        }

        #endregion Overrides
    }
}