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
    public class LoginFragment : BaseFragment<MainView, LoginViewModel>, GoogleApiClient.IConnectionCallbacks,
        GoogleApiClient.IOnConnectionFailedListener, ActivityCompat.IOnRequestPermissionsResultCallback
    {
        #region Variables
        static readonly int GET_ACCOUNTS = 0;

        protected override int FragmentId => Resource.Layout.LoginFragment;

        private FacebookAuthenticator _authFacebook;
        private GoogleUserModel _user;
        private GoogleApiClient _googleApiClient;
        private ConnectionResult _connectionResult;
        private Button _googleSignBtn;
        private bool _isIntentInProgress;
        private bool _isSignInClicked;
        private Button _facebookLoginButton;

        #endregion Variables

        #region Properties

        public string TAG { get; private set; }

        #endregion Properties

        #region Methods

        public void GetPermissions(object sender, EventArgs e)
        {
            if (ContextCompat.CheckSelfPermission(Activity, Manifest.Permission.GetAccounts) != (int)Permission.Granted)
            {
                RequestPermissions(new string[] { Manifest.Permission.GetAccounts }, GET_ACCOUNTS);

                return;
            }

            GoogleSignBtnClick();

            return;
        }

        public void OnConnected(Bundle connectionHint)
        {
            Android.Gms.Plus.Model.People.IPerson person = PlusClass.PeopleApi.GetCurrentPerson(_googleApiClient);

            string email = PlusClass.AccountApi.GetAccountName(_googleApiClient);
            string name = string.Empty;

            if (person != null)
            {
                _user.First_name = person.DisplayName;
                _user.Email = email;
                _user.Picture = person.Image.Url;
            }

            ViewModel.OnGoogleAuthenticationCompleted(_user);
        }

        private void GoogleSignBtnClick()
        {
            if (!_googleApiClient.IsConnecting)
            {
                _isSignInClicked = true;
                ResolveSignIn();
            }

            if (_googleApiClient.IsConnected)
            {
                PlusClass.AccountApi.ClearDefaultAccount(_googleApiClient);
                _googleApiClient.Disconnect();
            }
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            if (_isIntentInProgress)
            {
                return;
            }

            _connectionResult = result;

            if (_isSignInClicked)
            {
                ResolveSignIn();
            }
        }

        public void OnConnectionSuspended(int cause)
        {
        }

        private void OnFacebookLoginButtonClicked(object sender, EventArgs e)
        {
            _authFacebook = new FacebookAuthenticator(Configuration.ClientId, Configuration.Scope, ViewModel);

            Xamarin.Auth.OAuth2Authenticator authenticator = _authFacebook.GetAuthenticator();
            Intent intent = authenticator.GetUI(Activity);

            StartActivity(intent);
        }

        private void ResolveSignIn()
        {
            if (_googleApiClient.IsConnecting)
            {
                return;
            }

            if (!_connectionResult.HasResolution)
            {
                return;
            }

            try
            {
                _isIntentInProgress = true;
                StartIntentSenderForResult(_connectionResult.Resolution.IntentSender, 0, null, 0, 0, 0, null);
            }
            catch (IntentSender.SendIntentException)
            {
                _isIntentInProgress = false;
                _googleApiClient.Connect();
            }
        }

        #endregion Methods

        #region Overrides

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _facebookLoginButton = view.FindViewById<Button>(Resource.Id.facebookLoginButton);
            _facebookLoginButton.Click += OnFacebookLoginButtonClicked;

            _googleSignBtn = view.FindViewById<Button>(Resource.Id.sign_in_button);
            _googleSignBtn.Click += GetPermissions;

            _user = new GoogleUserModel();

            GoogleApiClient.Builder builder = new GoogleApiClient.Builder(Context);
            builder.AddConnectionCallbacks(this);
            builder.AddOnConnectionFailedListener(this);
            builder.AddApi(PlusClass.API);
            builder.AddScope(PlusClass.ScopePlusProfile);
            builder.AddScope(PlusClass.ScopePlusLogin);

            _googleApiClient = builder.Build();

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();

            _googleApiClient.Connect();
        }

        public override void OnStop()
        {
            base.OnStop();

            if (_googleApiClient.IsConnected)
            {
                _googleApiClient.Disconnect();
            }
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == GET_ACCOUNTS && grantResults[default(int)] == Permission.Granted)
            {
                GoogleSignBtnClick();

                return;
            }

            if (requestCode == GET_ACCOUNTS && grantResults[default(int)] != Permission.Granted)
            {
                Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(Constants.LocationPermissionNotGranted);
            }

            if (requestCode != GET_ACCOUNTS)
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode != default(int))
            {
                return;
            }

            if (resultCode != (int)Result.Ok)
            {
                _isSignInClicked = false;
            }

            _isIntentInProgress = false;

            if (!_googleApiClient.IsConnecting)
            {
                _googleApiClient.Connect();
            }
        }

        public override void OnDestroyView()
        {
            _facebookLoginButton.Click -= OnFacebookLoginButtonClicked;

            _googleSignBtn.Click -= GetPermissions;

            base.OnDestroyView();
        }

        #endregion Overrides
    }
}