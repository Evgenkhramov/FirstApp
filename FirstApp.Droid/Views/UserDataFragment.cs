using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V7;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FirstApp.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Android.Graphics;
using Android.Provider;
using MvvmCross.ViewModels;
using FirstApp.Core.Models;
using MvvmCross.Base;
using FirstApp.Droid.Services;
using static Android.Provider.MediaStore.Images;
using Android.Util;
using System.IO;
using Android.Media;
using Android.Database;
using Plugin.Permissions;
using System.Threading.Tasks;
using static Android.Manifest;
using Android.Support.V4.Content;
using Android;
using Android.Support.V4.App;
using Android.Support.Design.Widget;
using Plugin.Permissions.Abstractions;
using Permission = Plugin.Permissions.Abstractions.Permission;
using Plugin.CurrentActivity;
using FirstApp.Core.Interfaces;
using MvvmCross;
using MvvmCross.Plugin.PictureChooser;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame_new, false)]
    [Register("firstApp.Droid.Views.UserDataFragment")]
    public class UserDataFragment : BaseFragment<UserDataFragmentViewModel>
    {
        public static readonly int PickImageId = 1000;
        Button menuButton;
        Button btnCamera;
        ImageView cameraPreview;
        private string _imagePath;
        protected override int FragmentId => Resource.Layout.UserDataFragment;
        private IUserDialogService _userDialogService;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            menuButton = view.FindViewById<Button>(Resource.Id.menu_icon);
            btnCamera = view.FindViewById<Button>(Resource.Id.btnCamera);
            cameraPreview = view.FindViewById<ImageView>(Resource.Id.camera_preview);
            btnCamera.Click += GetPermissions;
            menuButton.Click += (object sender, EventArgs e) =>
            {
                OpenMenu();
            };
            //if (ContextCompat.CheckSelfPermission(this.Activity, Manifest.Permission.Camera) != (int)Android.Content.PM.Permission.Granted)
            //{
            //    ActivityCompat.RequestPermissions(this.Activity, new String[] { Manifest.Permission.Camera }, 1);
            //}
            //if (ContextCompat.CheckSelfPermission(this.Activity, Manifest.Permission.ReadExternalStorage) != (int)Android.Content.PM.Permission.Granted)
            //{
            //    ActivityCompat.RequestPermissions(this.Activity, new String[] { Manifest.Permission.ReadExternalStorage }, 1);
            //}

            return view;
        }

        //public string BitmapToString(Bitmap bitmap)
        //{

        //    byte[] bitmapData;
        //    using (var stream = new MemoryStream())
        //    {
        //        bitmap.Compress(Bitmap.CompressFormat.Png, 98, stream);
        //        bitmapData = stream.ToArray();
        //    }

        //    string imageString = Convert.ToBase64String(bitmapData);
        //    return imageString;
        //}

        //private static Bitmap ExifRotateBitmap(string filePath, Bitmap bitmap)
        //{
        //    if (bitmap == null)
        //        return null;

        //    var exif = new ExifInterface(filePath);
        //    var rotation = exif.GetAttributeInt(ExifInterface.TagOrientation, (int)Android.Media.Orientation.Normal);
        //    var rotationInDegrees = ExifToDegrees(rotation);
        //    if (rotationInDegrees == 0)
        //        return bitmap;

        //    using (var matrix = new Matrix())
        //    {
        //        matrix.PreRotate(rotationInDegrees);
        //        return Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);
        //    }
        //}

        //private static int ExifToDegrees(int exifOrientation)
        //{
        //    switch (exifOrientation)
        //    {
        //        case (int)Android.Media.Orientation.Rotate90:
        //            return 90;
        //        case (int)Android.Media.Orientation.Rotate180:
        //            return 180;
        //        case (int)Android.Media.Orientation.Rotate270:
        //            return 270;
        //        default:
        //            return 0;
        //    }
        //}

        //public static Bitmap ScaleDown(Bitmap realImage, string imagePath)
        //{
        //    bool filter = true;
        //    float maxImageSize = 300;
        //    float ratio = Math.Min(
        //            (float)maxImageSize / realImage.Width,
        //            (float)maxImageSize / realImage.Height);
        //    int width = (int)Math.Round((float)ratio * realImage.Width);
        //    int height = (int)Math.Round((float)ratio * realImage.Height);

        //    Bitmap newBitmap = Bitmap.CreateScaledBitmap(realImage, width, height, filter);
        //    Bitmap bitmap = ExifRotateBitmap(imagePath, newBitmap);
        //    return bitmap;
        //}

        //private void ButtonFromGallary(object sender, EventArgs eventArgs)
        //{

        //    if (ContextCompat.CheckSelfPermission(this.Activity, Manifest.Permission.ReadExternalStorage) == (int)Android.Content.PM.Permission.Granted)
        //    {
        //        Intent Intent = new Intent(Intent.ActionPick);
        //        Intent.SetType("image/*");
        //        Intent.SetAction(Intent.ActionGetContent);
        //        StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        //    }
        //    else
        //    {

        //        ActivityCompat.RequestPermissions(this.Activity, new String[] { Manifest.Permission.ReadExternalStorage }, 1);

        //        if (ContextCompat.CheckSelfPermission(this.Activity, Manifest.Permission.ReadExternalStorage) == (int)Android.Content.PM.Permission.Granted)
        //        {
        //            Intent Intent = new Intent(Intent.ActionPick);
        //            Intent.SetType("image/*");
        //            Intent.SetAction(Intent.ActionGetContent);
        //            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        //        }

        //    }
        //}

        public async void GetPermissions(object sender, System.EventArgs e)
        {
         
            if (await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage) != PermissionStatus.Granted ||
                      await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Camera) != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.Storage, Plugin.Permissions.Abstractions.Permission.Camera);
                //Best practice to always check that the key exists                    
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


        //public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        //{
        //    if (resultCode == (int)Result.Ok && requestCode != PickImageId)
        //    {
        //        base.OnActivityResult(requestCode, resultCode, data);
        //        Bitmap bitmap = (Bitmap)data.Extras.Get("data");
        //        //cameraPreview.SetImageBitmap(bitmap);
        //        ViewModel.SavePhoto(BitmapToString(bitmap));
        //    }
        //    if ((requestCode == PickImageId) && (resultCode == (int)Result.Ok) && (data != null))
        //    {
        //        Android.Net.Uri uri = data.Data;
        //        string pathImage = uri.EncodedPath;
        //        Bitmap imageBitmap = null;

        //        _imagePath = GetPathToImage(uri);

        //        imageBitmap = ScaleDown(Media.GetBitmap(this.Activity.ContentResolver, uri), _imagePath);

        //        ViewModel.SavePhoto(BitmapToString(imageBitmap));
        //    }
        //}

        //private string GetPathToImage(Android.Net.Uri uri)
        //{
        //    ICursor cursor = this.Activity.ContentResolver.Query(uri, null, null, null, null);
        //    cursor.MoveToFirst();
        //    string document_id = cursor.GetString(0);
        //    document_id = document_id.Split(':')[1];
        //    cursor.Close();

        //    cursor = Activity.ContentResolver.Query(
        //    Android.Provider.MediaStore.Images.Media.ExternalContentUri,
        //    null, MediaStore.Images.Media.InterfaceConsts.Id + " = ? ", new String[] { document_id }, null);
        //    cursor.MoveToFirst();
        //    string path = cursor.GetString(cursor.GetColumnIndex(MediaStore.Images.Media.InterfaceConsts.Data));
        //    cursor.Close();

        //    return path;
        //}

        //public void BtnCamera_Click(object sender, System.EventArgs e)
        //{
        //    if (ContextCompat.CheckSelfPermission(this.Activity, Manifest.Permission.Camera) == (int)Android.Content.PM.Permission.Granted)
        //    {
        //        Intent intent = new Intent(MediaStore.ActionImageCapture);
        //        StartActivityForResult(intent, 0);
        //    }
        //    else
        //    {
        //        ActivityCompat.RequestPermissions(this.Activity, new String[] { Manifest.Permission.Camera }, 1);

        //        if (ContextCompat.CheckSelfPermission(this.Activity, Manifest.Permission.Camera) == (int)Android.Content.PM.Permission.Granted)
        //        {
        //            Intent intent = new Intent(MediaStore.ActionImageCapture);
        //            StartActivityForResult(intent, 0);
        //        }
        //    }
        //}

        public void ChoosePhoto()
        {
            SelectPhoto("Select Photo", "Please, select photo.", "From memory", "From camera");
        }
      



        //private async void BtnCamera_Click(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
        //        if (status != PermissionStatus.Granted)
        //        {
        //            if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
        //            {
        //                _userDialogService.ShowAlertForUser("Need location", "Gunna need that location", "OK");
        //            }

        //            var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
        //            //Best practice to always check that the key exists
        //            if (results.ContainsKey(Permission.Storage))
        //                status = results[Permission.Storage];
        //        }

        //        if (status == PermissionStatus.Granted)
        //        {
        //            Intent intent = new Intent(MediaStore.ActionImageCapture);
        //            StartActivityForResult(intent, 0);
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}

    }
}