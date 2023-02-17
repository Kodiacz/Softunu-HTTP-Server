namespace MvcApp.Services
{
    public interface IUserService
    {
        void CreateUser(string username, string email, string password);

        object GetUserId(string username, string password);

        bool IsUsernameAvailable(string username);

        bool IsEmailAvailable(string email);
    }
}
