using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FirstApp.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;
using Android.Content;
using Android.Support.V4.Content;
using Android;
using Android.Content.PM;
using Acr.UserDialogs.Infrastructure;
using FirstApp.Droid.Interfaces;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame_new, true)]
    [Register("firstApp.Droid.Views.TaskDetailsFragment")]
    public class TaskDetailsFragment : BaseFragment<TaskDetailsViewModel>, IBackButtonListener
    {
        static readonly int READ_EXTERNAL_STORAGE = 0;
        static readonly int ACCESS_COARSE_LOCATION = 1;
        private int _fileCode = 1000;
        private int _mapCode = 1100;

        public Button GetFileButton;
        public Button GetMarkerButton;
        public Button MenuButton;
        protected override int FragmentId => Resource.Layout.TaskDetailsFragment;
        public string TAG { get; private set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            GetMarkerButton = view.FindViewById<Button>(Resource.Id.getMapMarker);
            GetMarkerButton.Click += (object sender, EventArgs e) =>
              {
                  GetMapPositionPermissions(sender, e);
              };

            GetFileButton = view.FindViewById<Button>(Resource.Id.getFileButton);
            GetFileButton.Click += (object sender, EventArgs e) =>
            {
                GetStoragePermissions(sender, e);
            };

            return view;
        }

        public void GetMapPositionPermissions(object sender, EventArgs e)
        {
            if (ContextCompat.CheckSelfPermission(this.Activity, Manifest.Permission.AccessCoarseLocation) != (int)Permission.Granted)
            {
                RequestPermissions(new String[] { Manifest.Permission.AccessCoarseLocation }, ACCESS_COARSE_LOCATION);
            }

            if (ContextCompat.CheckSelfPermission(this.Context, Manifest.Permission.AccessCoarseLocation) == (int)Permission.Granted)
            {
                AddMarker();
            }
        }

        public void GetStoragePermissions(object sender, System.EventArgs e)
        {
            if (ContextCompat.CheckSelfPermission(this.Activity, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted)
            {
                RequestPermissions(new String[] { Manifest.Permission.ReadExternalStorage }, READ_EXTERNAL_STORAGE);
            }

            if (ContextCompat.CheckSelfPermission(this.Context, Manifest.Permission.ReadExternalStorage) == (int)Permission.Granted)
            {
                OpenFile();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == READ_EXTERNAL_STORAGE)
            {
                Log.Info(TAG, "Received response for Location permission request.");

                if ((grantResults.Length == 1) && (grantResults[0] == Permission.Granted))
                {
                    OpenFile();
                }
                else
                {
                    Log.Info(TAG, "Location permission was NOT granted.");
                }
            }
            if (requestCode == ACCESS_COARSE_LOCATION)
            {
                Log.Info(TAG, "Received response for Coarse location permission request.");

                if ((grantResults.Length == 1) && (grantResults[0] == Permission.Granted))
                {
                    AddMarker();
                }
                else
                {
                    Log.Info(TAG, "Coarse location permission was NOT granted.");
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }

        public void AddMarker()
        {
            ViewModel.AddMarkerCommand.Execute();
        }

        public void OpenFile()
        {
            Intent intent = new Intent();
            intent.SetType("*/*");
            intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(intent, "Select File"), _fileCode);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            if (requestCode == _fileCode)
            {
                string fileName = null;

                if (resultCode == (int)Result.Ok)
                {
                    System.Uri uri = new System.Uri(data.DataString);

                    fileName = System.IO.Path.GetFileNameWithoutExtension(uri.LocalPath);

                    ViewModel.SaveFileName(fileName);
                }
            }
        }

        public void OnBackPressed()
        {
            ViewModel.BackCommand.Execute();
        }

        public override void OnResume()
        {
            base.OnResume();
        }
    }
}