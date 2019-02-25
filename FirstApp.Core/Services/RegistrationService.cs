using System;
using System.Collections.Generic;
using System.Text;
using FirstApp.Core.Interfaces;
using Plugin.SecureStorage;
using FirstApp.Core;

namespace FirstApp.Core.Services
{
    class RegistrationService : IRegistrationService
    {
        public void UserRegistration(string name, string password)
        {
            CrossSecureStorage.Current.SetValue(Constants.SequreKeyForUserName, name);
            CrossSecureStorage.Current.SetValue(Constants.SequreKeyForUserPassword, password);
            CrossSecureStorage.Current.SetValue(Constants.SequreKeyForLoged, Constants.LogIn);

        }
    }
}
