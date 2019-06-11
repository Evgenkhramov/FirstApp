using FirstApp.Core.Models;

namespace FirstApp.Core.Interfaces
{
    public interface IRegistrationService
    {
        void SaveDataInSequreStorage(string id);
        int SaveUserFromApp(string registrationUserName, string registrationUserPassword, string userEmail, LoginType loginType);
        int SaveUserFromSocialNetworks(string registrationUserName, string userEmail, double userIdFromSocialNetworks,
            string surname, string photoUrl, string userPhoto, LoginType loginType);
    }
}
