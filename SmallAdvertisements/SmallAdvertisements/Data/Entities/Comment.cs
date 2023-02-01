namespace SmallAdvertisements.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Body { get; set; }

        public DateTime Date { get; set; }

        public IdentityUser Author { get; set; }
        public int AdvertisementId { get; set; }

        public Advertisement Advertisement { get; set; }


    }
}
