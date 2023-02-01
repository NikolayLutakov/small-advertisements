namespace SmallAdvertisements.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class Advertisement
    {
        public Advertisement()
        {
            this.Likes = new HashSet<Like>();
            this.Comments = new HashSet<Comment>();
        }
        public int Id { get; set; }

        [Required]
        public IdentityUser Author { get; set; }

        [MaxLength(20)]
        [Required]
        public string Title { get; set; }

        [MaxLength(250)]
        [Required]
        public string Body { get; set; }

        public DateTime Date { get; set; }


        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }

        



    }
}
