﻿using Android.App;

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

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame_new, true)]
    [Register("firstApp.Droid.Views.TaskDetailsFragment")]
    public class TaskDetailsFragment : BaseFragment<TaskDetailsViewModel>
    {
        static readonly int READ_EXTERNAL_STORAGE = 0;
        private int fileCode = 1000;
        public Button getFileButton;
        public Button menuButton;
        protected override int FragmentId => Resource.Layout.TaskDetailsFragment;
        public string TAG { get; private set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            getFileButton = view.FindViewById<Button>(Resource.Id.getFileButton);
            getFileButton.Click += (object sender, EventArgs e) =>
            {
                GetPermissions(sender, e);
            };

            //menuButton = view.FindViewById<Button>(Resource.Id.menu_icon);
            //menuButton.Click += (object sender, EventArgs e) =>
            //{
            //    OpenMenu();
            //};

            return view;
        }

        public void GetPermissions(object sender, System.EventArgs e)
        {
            if (ContextCompat.CheckSelfPermission(this.Activity, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted)
            {
                RequestPermissions(new String[] { Manifest.Permission.ReadExternalStorage }, READ_EXTERNAL_STORAGE);

            }
            else if (ContextCompat.CheckSelfPermission(this.Context, Manifest.Permission.ReadExternalStorage) == (int)Permission.Granted)
            {
                OpenFile();
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == READ_EXTERNAL_STORAGE)
            {
                // Received permission result for camera permission.
                Log.Info(TAG, "Received response for Location permission request.");

                // Check if the only required permission has been granted
                if ((grantResults.Length == 1) && (grantResults[0] == Permission.Granted))
                {
                    // Location permission has been granted, okay to retrieve the location of the device.
                    OpenFile();
                }
                else
                {
                    Log.Info(TAG, "Location permission was NOT granted.");
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }

        public void OpenFile()
        {
            Intent intent = new Intent();
            intent.SetType("*/*");
            intent.SetAction(Intent.ActionGetContent);
            //intent.AddCategory(Intent.CategoryOpenable);
            StartActivityForResult(Intent.CreateChooser(intent, "Select File"), fileCode);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            if (requestCode == fileCode)
            {
                string fileName = null;

                if (resultCode == (int)Result.Ok)
                {
                    //fileName = data.Data.LastPathSegment;
                    System.Uri uri = new System.Uri(data.DataString);

                    fileName = System.IO.Path.GetFileNameWithoutExtension(uri.LocalPath);

                    ViewModel.FileName += $"{fileName},\n";
                }
            }
        }

        public override void OnResume()
        {
            base.OnResume();
        }
    }
}