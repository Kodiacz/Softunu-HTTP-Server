namespace MvcApp.Data
{
    using SoftuniHTTPServer.MvcFramework;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Repositories = new HashSet<Repository>();
            this.Commits = new HashSet<Commit>();
            this.Role = IdentityRole.Admin;
        }

        public ICollection<Repository> Repositories { get; set; }

        public ICollection<Commit> Commits { get; set; }
    }
}
