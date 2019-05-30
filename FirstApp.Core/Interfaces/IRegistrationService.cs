using FirstApp.Core.Models;

namespace FirstApp.Core.Interfaces
{
    public interface IRegistrationService
    {
        void UserRegistration(string id);
        int SaveUserInDbFromApp(string registrationUserName, string registrationUserPassword, string userEmail, LoginType loginType);
        int SaveUserInDbFromSocialNetworks(string registrationUserName, string userEmail, double userIdFromSocialNetworks,
            string surname, string photoUrl, string userPhoto, LoginType loginType);
    }
}
