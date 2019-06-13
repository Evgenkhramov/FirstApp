using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views.InputMethods;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using FirstApp.Core;
using FirstApp.Core.Models;
using FirstApp.Core.ViewModels;
using FirstApp.Droid.Interfaces;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Plugin.Messenger;
using Plugin.SecureStorage;

namespace FirstApp.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "MainView",
        Theme = "@style/AppTheme",
        NoHistory = false,
        LaunchMode = LaunchMode.SingleTask,
        Name = "FirstApp.Droid.Views.MainView"
        )]
    public class MainView : MvxAppCompatActivity<MainViewModel>, IMainView
    {
        #region Properties

        private IMvxNavigationService _navigationService;

        private IMvxMessenger _messenger;

        private readonly string _oneSignal = "30876c39-f218-456b-b1a9-89e54fa62d9e";

        public DrawerLayout DrawerLayout { get; set; }

        #endregion Properties

        #region Methods

        public void HideSoftKeyboard()
        {
            if (CurrentFocus == null)
            {
                return;
            }

            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);

            CurrentFocus.ClearFocus();
        }

        public void CloseApplication()
        {
            Finish();
        }

        public async Task CloseDrawer()
        {
            DrawerLayout.CloseDrawers();

            await Task.Delay(TimeSpan.FromMilliseconds(Constants.TimeOutLong));
        }

        public async Task OpenDrawer()
        {
            DrawerLayout.OpenDrawer(GravityCompat.Start);

            await Task.Delay(TimeSpan.FromMilliseconds(Constants.TimeOutLong));
        }

        private void HandleNotificationOpened(OSNotificationOpenedResult result)
        {
            OSNotificationPayload payload = result.notification.payload;
            Dictionary<string, object> additionalData = payload.additionalData;
            string message = payload.body;

            bool hasViewModel = additionalData.ContainsKey("User");
            bool hasTaskId = additionalData.ContainsKey("TaskId");

            if (hasViewModel)
            {
                CrossSecureStorage.Current.SetValue("User", "user");

                return;
            }

            if (hasTaskId)
            {


                var taskObj = additionalData["TaskId"];

                string taskId = Convert.ToString(taskObj);

                CrossSecureStorage.Current.SetValue("TaskId", taskId);

                return;
            }

            ViewModel.ShowMain();
        }

        private void SendPushMessege(string messageData)
        {
            var message = new TaskPushMessage(this, messageData);

            _messenger.Publish(message);
        }

        #endregion Methods

        #region Overrides

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();

            _messenger = Mvx.IoCProvider.Resolve<IMvxMessenger>();

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);

            SetContentView(Resource.Layout.MainView);

            OneSignal.Current.StartInit(_oneSignal).HandleNotificationOpened(HandleNotificationOpened).EndInit();

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            UserDialogs.Init(this);

            if (CrossSecureStorage.Current.HasKey("TaskId"))
            {
                string taskId = CrossSecureStorage.Current.GetValue("TaskId");

                CrossSecureStorage.Current.DeleteKey("TaskId");

                _navigationService.Navigate<TaskListViewModel>();
                _navigationService.Navigate<MenuViewModel>();

                SendPushMessege(taskId);

                return;
            }

            if (CrossSecureStorage.Current.HasKey("User"))
            {
                CrossSecureStorage.Current.DeleteKey("User");
                _navigationService.Navigate<UserDataViewModel>();
                _navigationService.Navigate<MenuViewModel>();

                return;
            }

            if (bundle == null)
            {
                ViewModel.ShowMain();
            }
        }

        public override void OnBackPressed()
        {
            if (DrawerLayout != null && DrawerLayout.IsDrawerOpen(GravityCompat.Start))
            {
                DrawerLayout.CloseDrawers();

                return;
            }

            Android.Support.V4.App.Fragment currentFragment = SupportFragmentManager.FindFragmentById(Resource.Id.content_frame_new);

            IBackButtonListener listener = currentFragment as IBackButtonListener;

            if (listener != null)
            {
                listener.HandleBackPressed();
                return;
            }

            CloseApplication();
        }

        #endregion Overrides
    }
}
