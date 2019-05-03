namespace FirstApp.Core.Interfaces
{
    public interface IAuthorizationService
    {
        bool IsLoggedIn(string userName, string userPassword);
    }
}
