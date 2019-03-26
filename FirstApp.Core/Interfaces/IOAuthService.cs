using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FirstApp.Core.Interfaces
{
    public interface IOAuthService
    {
        Task<LoginResult> Login();
        void Logout();
    }
}
