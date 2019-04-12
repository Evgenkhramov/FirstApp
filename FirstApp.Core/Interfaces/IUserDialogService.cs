using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FirstApp.Core.Interfaces
{
    public interface IUserDialogService
    {
        void ShowAlertForUser(string title, string messege, string okbtnText);
        void ChoosePhoto(string title, string messege, string okbtnText, string escbtnText);
        Task<string> ShowAlertForUserWithSomeLogic(string title, string message, string okbtnText, string nobtnText);
    }
}
