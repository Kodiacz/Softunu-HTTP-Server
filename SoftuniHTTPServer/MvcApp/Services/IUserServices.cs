namespace MvcApp.Services
{
    public interface IUserServices
    {
        void CreateUser(string username, string email, string password);

        object GetUserId(string username, string password);

        bool IsUsernameAvailable(string username);

        bool IsEmailAvailable(string email);
    }
}
