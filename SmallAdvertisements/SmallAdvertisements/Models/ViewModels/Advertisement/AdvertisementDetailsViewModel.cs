using Microsoft.AspNetCore.Mvc.Infrastructure;
using SmallAdvertisements.Models.ServiceModels.Comment.Output;
using SmallAdvertisements.Models.ServiceModels.Like.Output;

namespace SmallAdvertisements.Models.ViewModels.Advertisement
{
    public class AdvertisementDetailsViewModel
    {
        public int Id { get; set; }
        public string AdvertisementTitle { get; set; }

        public string Body { get; set; }

        public string Author { get; set; }

        public string DateAdded { get; set; }

        public int LikesCount { get; set; }

        public List<CommentOutputModel> Comments { get; set; }

        public List<LikeOutputModel> Likes { get; set; }
    }
}
