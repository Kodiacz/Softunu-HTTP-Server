using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcApp.Data
{
    public class Commit
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(Creator))]
        public string? CreatorId { get; set; }
        public User? Creator { get; set; }


        [ForeignKey(nameof(Repository))]
        public string? RepositoryId { get; set; }
        public Repository? Repository { get; set; }
    }
}