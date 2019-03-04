using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.Interfaces
{
    public interface IUserDialogService
    {
        void ShowAlertForUser(string title, string messege, string okbtnText);
    }
}
