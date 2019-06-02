using Acr.UserDialogs;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Plus;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using FirstApp.Core;
using FirstApp.Core.Models;
using FirstApp.Core.Providers;
using FirstApp.Core.ViewModels;
using MvvmCross;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(StartViewModel), Resource.Id.content_frame_new, false)]
    [Register("firstApp.Droid.Views.LoginFragment")]
    public class LoginFragment : BaseFragment<LoginViewModel>, GoogleApiClient.IConnectionCallbacks,
        GoogleApiClient.IOnConnectionFailedListener, ActivityCompat.IOnRequestPermissionsResultCallback
    {
        #region Variables

        protected override int FragmentId => Resource.Layout.LoginFragment;
        private FacebookAuthenticator _authFacebook;
        static readonly int GET_ACCOUNTS = 0;
        private GoogleModel _user;
        private GoogleApiClient _mGoogleApiClient;
        private ConnectionResult _mConnectionResult;
        private Button _mGsignBtn;
        private bool _mIntentInProgress;
        private bool _mSignInClicked;
        private bool _mInfoPopulated;
        private Button _facebookLoginButton;

        #endregion Variables

        #region Properties

        public string TAG { get; private set; }

        #endregion Properties

        #region Methods

        public void GetPermissions(object sender, EventArgs e)
        {
            if (ContextCompat.CheckSelfPermission(this.Activity, Manifest.Permission.GetAccounts) != (int)Permission.Granted)
            {
                RequestPermissions(new String[] { Manifest.Permission.GetAccounts }, GET_ACCOUNTS);
                return;
            }

            MGsignBtn_Click();
            return;
        }

        public void OnConnected(Bundle connectionHint)
        {
            Android.Gms.Plus.Model.People.IPerson person = PlusClass.PeopleApi.GetCurrentPerson(_mGoogleApiClient);
            string email = PlusClass.AccountApi.GetAccountName(_mGoogleApiClient);
            string name = string.Empty;
            if (person != null)
            {
                _user.First_name = person.DisplayName;
                _user.Email = email;
                _user.Picture = person.Image.Url;
            }
            ViewModel.OnGoogleAuthenticationCompleted(_user);
        }

        private void MGsignBtn_Click()
        {
            if (!_mGoogleApiClient.IsConnecting)
            {
                _mSignInClicked = true;
                ResolveSignIn();

            }
            if (_mGoogleApiClient.IsConnected)
            {
                PlusClass.AccountApi.ClearDefaultAccount(_mGoogleApiClient);
                _mGoogleApiClient.Disconnect();
            }
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            if (!_mIntentInProgress)
            {
                _mConnectionResult = result;

                if (_mSignInClicked)
                {
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
            Xamarin.Auth.OAuth2Authenticator authenticator = _authFacebook.GetAuthenticator();
            Intent intent = authenticator.GetUI(this.Activity);
            StartActivity(intent);
        }

        private void ResolveSignIn()
        {
            if (_mGoogleApiClient.IsConnecting)
            {
                return;
            }
            if (_mConnectionResult.HasResolution)
            {
                try
                {
                    _mIntentInProgress = true;
                    StartIntentSenderForResult(_mConnectionResult.Resolution.IntentSender, 0, null, 0, 0, 0, null);
                }
                catch (IntentSender.SendIntentException io)
                {
                    _mIntentInProgress = false;
                    _mGoogleApiClient.Connect();
                }
            }
        }

        #endregion Methods

        #region Overrides

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _facebookLoginButton = view.FindViewById<Button>(Resource.Id.facebookLoginButton);
            _facebookLoginButton.Click += OnFacebookLoginButtonClicked;

            _mGsignBtn = view.FindViewById<Button>(Resource.Id.sign_in_button);
            _mGsignBtn.Click += GetPermissions;

            _user = new GoogleModel();

            GoogleApiClient.Builder builder = new GoogleApiClient.Builder(Context);
            builder.AddConnectionCallbacks(this);
            builder.AddOnConnectionFailedListener(this);
            builder.AddApi(PlusClass.API);
            builder.AddScope(PlusClass.ScopePlusProfile);
            builder.AddScope(PlusClass.ScopePlusLogin);

            _mGoogleApiClient = builder.Build();

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();
            _mGoogleApiClient.Connect();
        }

        public override void OnStop()
        {
            base.OnStop();
            if (_mGoogleApiClient.IsConnected)
            {
                _mGoogleApiClient.Disconnect();
            }
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == GET_ACCOUNTS)
            {
                Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(Constants.LocationPermission);

                if ((grantResults.Length == 1) && (grantResults[0] == Permission.Granted))
                {
                    MGsignBtn_Click();
                }
                else
                {
                    Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(Constants.LocationPermissionNotGranted);
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
           
            if (requestCode == 0)
            {
                if (resultCode != (int)Result.Ok)
                {
                    _mSignInClicked = false;
                }
                _mIntentInProgress = false;
                if (!_mGoogleApiClient.IsConnecting)
                {
                    _mGoogleApiClient.Connect();
                }
            }
        }

        public override void OnDestroyView()
        {
            _facebookLoginButton.Click -= OnFacebookLoginButtonClicked;

            _mGsignBtn.Click -= GetPermissions;

            base.OnDestroyView();
        }

        #endregion Overrides
    }
}