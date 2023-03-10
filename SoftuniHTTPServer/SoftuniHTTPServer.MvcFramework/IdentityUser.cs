using System.ComponentModel.DataAnnotations;

namespace SoftuniHTTPServer.MvcFramework
{
    public class IdentityUser<T>
    {
        [Key]
        public T Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public IdentityRole Role { get; set; }

    }
}
