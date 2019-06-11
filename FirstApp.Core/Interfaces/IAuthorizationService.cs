namespace FirstApp.Core.Interfaces
{
    public interface IAuthorizationService
    {
        bool CheckLoggedIn(string userName, string userPassword);
    }
}
