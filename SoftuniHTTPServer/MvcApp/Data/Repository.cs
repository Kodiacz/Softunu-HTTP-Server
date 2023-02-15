namespace MvcApp.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Repository
    {
        public Repository()
        {
            this.Commits = new HashSet<Commit>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; }
        public User Owner { get; set; }

        public ICollection<Commit> Commits { get; set; }
    }
}