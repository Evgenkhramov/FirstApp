using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.Services
{
    public interface IAuthorizationService
    {
        bool IsLoggedIn(string userName, string userPassword);

    }
}
