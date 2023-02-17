namespace MvcApp.Services
{
    using MvcApp.Data;
    using SoftuniHTTPServer.MvcFramework;
    using System.Security.Cryptography;
    using System.Text;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            //// This is a bad practice and this class (UserServices) 
            //// can't choose what type should the dbContext be.
            //// The type of the dbContext should come from outside this
            //// class (dependancy inversion). And along thah it comes the 
            //// other principle (lisov substitution) the parameter that comes
            //// should not break the application, this means if the
            //// type is interface or abstract class then the concrete type
            //// should replace it without any problems:
            // this.dbContext = new ApplicationDbContext();

            // it should be like this:
            this.dbContext = dbContext;
        }

        public void CreateUser(string username, string email, string password)
        {
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Username = username,
                Email = email,
                Password = ComputeHash(password),
                Role = IdentityRole.User
            };

            this.dbContext.Add(user);
            this.dbContext.SaveChangesAsync();
        }

        public object GetUserId(string username, string password)
        {
            var user = this.dbContext.Users.FirstOrDefault(x => x.Username == username);
            return user?.Id;
        }

        public bool IsEmailAvailable(string email)
        {
            return !this.dbContext.Users.Any(u => u.Email == email);
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this.dbContext.Users.Any(u => u.Username == username);
        }

        public static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
            {
                hashedInputStringBuilder.Append(b.ToString("X2"));
            }
            return hashedInputStringBuilder.ToString();
        }
    }
}

