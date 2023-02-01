namespace SmallAdvertisements.Models.ServiceModels.Comment.Output
{
    using Microsoft.AspNetCore.Identity;
    using SmallAdvertisements.Models.ServiceModels.Advertisement.Output;
    using System.ComponentModel.DataAnnotations;

    public class CommentOutputModel
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }

        public IdentityUser Author { get; set; }
        public int AdvertisementId { get; set; }
    }
}
