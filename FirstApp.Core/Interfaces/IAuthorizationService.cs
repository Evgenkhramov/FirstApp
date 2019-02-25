using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.Interfaces
{
    public interface IAuthorizationService
    {
        bool IsLoggedIn(string userName, string userPassword);

    }
}
