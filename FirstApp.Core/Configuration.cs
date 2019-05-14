using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core
{
    public static class Configuration
    {
        public const string ClientId = "305583930124967";
        public const string ClientIdGoogle = "106414763437-s35lsgtqq4pbu8dvn8rv3ckagmkdp8t5.apps.googleusercontent.com";     
        public const string Scope = "email";
        public const string GoogleScope = "https://www.googleapis.com/auth/userinfo.email";
        public const string RedirectUrl = "com.firstapp.xamarin:/oauth2redirect";
        public const string iOSRedirectUrlGoogle = "com.firstapp.xamarin:/oauth2redirect";
    }
}
