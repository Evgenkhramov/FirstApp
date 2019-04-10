using Android.App;

using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FirstApp.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;
using Android.Net;
using Android.Content;


namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame_new, true)]
    [Register("firstApp.Droid.Views.TaskDetailsFragment")]
    public class TaskDetailsFragment : BaseFragment<TaskDetailsViewModel>
    {
        private int fileCode = 1000;
        public Button getFileButton;
        public Button menuButton;
        protected override int FragmentId => Resource.Layout.TaskDetailsFragment;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            getFileButton = view.FindViewById<Button>(Resource.Id.getFileButton);
            getFileButton.Click += (object sender, EventArgs e) =>
            {
                OpenFile();
            };

            menuButton = view.FindViewById<Button>(Resource.Id.menu_icon);
            menuButton.Click += (object sender, EventArgs e) =>
            {
                OpenMenu();
            };

            return view;
        }

        public string OpenFile()
        {
            string fileName = "Jeck";
            //var intent = new Intent(Activity, typeof(StartActivity));

           // Intent intent = new Intent(Activity,Intent.ActionGetContent);
            Intent intent = new Intent();
            intent.SetType("*/*");
            intent.SetAction(Intent.ActionGetContent);
            //intent.AddCategory(Intent.CategoryOpenable);
            StartActivityForResult(Intent.CreateChooser(intent, "Select File"), fileCode);
            return fileName;
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            if (requestCode == fileCode)
            {
                string fileName = null;

                if (resultCode == (int)Result.Ok)
                {

                    string uri = data.DataString;
                  
                    int cut = uri.LastIndexOf('/');
                    if (cut != -1)
                    {
                        fileName = uri.Substring(cut + 1);
                    }
                }

                ViewModel.FileName += $"{fileName},";              
            }
        }
    }
}