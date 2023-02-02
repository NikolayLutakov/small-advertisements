namespace SmallAdvertisements.Models.ServiceModels.Like.Output
{
    using Microsoft.AspNetCore.Identity;
    using Data.Entities;
    using SmallAdvertisements.Models.ServiceModels.Advertisement.Output;

    public class LikeOutputModel
    {
        public int Id { get; set; }

        public IdentityUser Author { get; set; }

        public int AdvertisementId { get; set; }

        public string AdvertisementTitle { get; set; }


    }
}
