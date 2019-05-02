using System;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.Gms.Plus;
using Android.Util;
using Android.Graphics;
using System.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FirstApp.Core;
using FirstApp.Core.Authentication;
using FirstApp.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System.IO;
using Android.App;
using Android.Content;
using Android;
using Android.Support.V4.Content;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Gms.Auth.Api;
using FirstApp.Core.Models;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(StartViewModel), Resource.Id.content_frame_new, false)]
    [Register("firstApp.Droid.Views.LoginFragment")]
    public class LoginFragment : BaseFragment<LoginViewModel>, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener, ActivityCompat.IOnRequestPermissionsResultCallback
    {
        protected override int FragmentId => Resource.Layout.LoginFragment;
        private FacebookAuthenticator _authFacebook;
        static readonly int GET_ACCOUNTS = 0;
        private GoogleModel user = new GoogleModel();

        GoogleApiClient mGoogleApiClient;
        private ConnectionResult mConnectionResult;
        Button mGsignBtn;
        TextView TxtName, TxtEmail;
        ImageView ImgProfile;
        private bool mIntentInProgress;
        private bool mSignInClicked;
        private bool mInfoPopulated;
        public string TAG { get; private set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var facebookLoginButton = view.FindViewById<Button>(Resource.Id.facebookLoginButton);
            facebookLoginButton.Click += OnFacebookLoginButtonClicked;

            mGsignBtn = view.FindViewById<Button>(Resource.Id.sign_in_button);
            mGsignBtn.Click += GetPermissions;

            GoogleApiClient.Builder builder = new GoogleApiClient.Builder(this.Context);
            builder.AddConnectionCallbacks(this);
            builder.AddOnConnectionFailedListener(this);
            builder.AddApi(PlusClass.API);
            builder.AddScope(PlusClass.ScopePlusProfile);
            builder.AddScope(PlusClass.ScopePlusLogin);
            //Build our IGoogleApiClient  
            mGoogleApiClient = builder.Build();

            return view;
        }
        public override void OnStart()
        {
            base.OnStart();
            mGoogleApiClient.Connect();
        }
        public override void OnStop()
        {
            base.OnStop();
            if (mGoogleApiClient.IsConnected)
            {
                mGoogleApiClient.Disconnect();
            }
        }

     
        public void GetPermissions(object sender, System.EventArgs e)
        {
            if (ContextCompat.CheckSelfPermission(this.Activity, Manifest.Permission.GetAccounts) != (int)Permission.Granted)
            {
                RequestPermissions( new String[] { Manifest.Permission.GetAccounts }, GET_ACCOUNTS);
                
            }
            else if (ContextCompat.CheckSelfPermission(this.Context, Manifest.Permission.GetAccounts) == (int)Permission.Granted)
            {
                MGsignBtn_Click();
            }
         
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == GET_ACCOUNTS)
            {
                // Received permission result for camera permission.
                Log.Info(TAG, "Received response for Location permission request.");

                // Check if the only required permission has been granted
                if ((grantResults.Length == 1) && (grantResults[0] == Permission.Granted))
                {
                    // Location permission has been granted, okay to retrieve the location of the device.
                    MGsignBtn_Click();
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

        public void OnConnected(Bundle connectionHint)
        {
            
            var person = PlusClass.PeopleApi.GetCurrentPerson(mGoogleApiClient);
            var email = PlusClass.AccountApi.GetAccountName(mGoogleApiClient);
            var name = string.Empty;
            if (person != null)
            {
                
                user.First_name = person.DisplayName;
                user.Email = email;
                user.Picture = person.Image.Url;
                
                //var Img = person.Image.Url;
                //var imageBitmap = GetImageBitmapFromUrl(Img.Remove(Img.Length - 5));
                //if (imageBitmap != null) ImgProfile.SetImageBitmap(imageBitmap);
            }
             ViewModel.OnGoogleAuthenticationCompleted(user);
        }

        private void MGsignBtn_Click()
        {
            if (!mGoogleApiClient.IsConnecting)
            {
                mSignInClicked = true;
                //ViewModel.OnGoogleAuthenticationCompleted(user);
                ResolveSignIn();

            }
            else if (mGoogleApiClient.IsConnected)
            {
                PlusClass.AccountApi.ClearDefaultAccount(mGoogleApiClient);
                mGoogleApiClient.Disconnect();
            }
        }
        private void ResolveSignIn()
        {
            if (mGoogleApiClient.IsConnecting)
            {
                return;
            }
            if (mConnectionResult.HasResolution)
            {
                try
                {
                    mIntentInProgress = true;
                    StartIntentSenderForResult(mConnectionResult.Resolution.IntentSender, 0, null, 0, 0, 0, null);
                }
                catch (Android.Content.IntentSender.SendIntentException io)
                {
                    mIntentInProgress = false;
                    mGoogleApiClient.Connect();
                }
            }
        }
        private Bitmap GetImageBitmapFromUrl(String url)
        {
            Bitmap imageBitmap = null;
            try
            {
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }
                return imageBitmap;
            }
            catch (IOException e) { }
            return null;
        }
        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Log.Debug(TAG, "onActivityResult:" + requestCode + ":" + resultCode + ":" + data);
            if (requestCode == 0)
            {
                if (resultCode != (int)Result.Ok)
                {
                    mSignInClicked = false;
                }
                mIntentInProgress = false;
                if (!mGoogleApiClient.IsConnecting)
                {
                    mGoogleApiClient.Connect();
                }
            }
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            if (!mIntentInProgress)
            {
                //Store the ConnectionResult so that we can use it later when the user clicks 'sign-in;
                mConnectionResult = result;

                if (mSignInClicked)
                {
                    //The user has already clicked 'sign-in' so we attempt to resolve all
                    //errors until the user is signed in, or the cancel
                    ResolveSignIn();
                }
            }
        }

        public void OnConnectionSuspended(int cause)
        {
        }

        private void OnFacebookLoginButtonClicked(object sender, EventArgs e)
        {
            _authFacebook = new FacebookAuthenticator(Configuration.ClientId, Configuration.Scope, ViewModel);
            var authenticator = _authFacebook.GetAuthenticator();
            var intent = authenticator.GetUI(this.Activity);
            //intent.SetFlags(ActivityFlags.NoHistory);
            StartActivity(intent);
        }
        
    }
}