﻿namespace MvcApp.Services
{
    using MvcApp.Data;
    using SoftuniHTTPServer.MvcFramework;
    using System.Security.Cryptography;
    using System.Text;

    public class UserServices : IUserServices
    {
        private readonly ApplicationDbContext dbContext;

        public UserServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateUser(string username, string email, string password)
        {
            var user = new User() 
            { 
                Username = username, 
                Email = email, 
                Password = ComputeHash(password), 
                Role = IdentityRole.User
            };
            await this.dbContext.Users.AddAsync(user);
            await this.dbContext.SaveChangesAsync();
        }

        public bool IsUserValid(string username, string password)
        {
            var user = this.dbContext.Users.FirstOrDefault(x => x.Username == username);
            return user?.Password == ComputeHash(password);
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

