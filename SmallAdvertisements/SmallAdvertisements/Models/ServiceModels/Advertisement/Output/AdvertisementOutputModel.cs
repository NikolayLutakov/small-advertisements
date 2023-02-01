namespace SmallAdvertisements.Models.ServiceModels.Advertisement.Output
{
    using Microsoft.AspNetCore.Identity;
    using SmallAdvertisements.Data.Entities;
    using SmallAdvertisements.Models.ServiceModels.Comment.Output;
    using SmallAdvertisements.Models.ServiceModels.Like.Output;
    using System.ComponentModel.DataAnnotations;

    public class AdvertisementOutputModel
    {
        public int Id { get; set; }

     
        public IdentityUser Author { get; set; }

        
        public string Title { get; set; }

      
        public string Body { get; set; }

        public DateTime Date { get; set; }


        public ICollection<LikeOutputModel> Likes { get; set; }
        public ICollection<CommentOutputModel> Comments { get; set; }

    }
}
