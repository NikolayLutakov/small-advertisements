namespace SmallAdvertisements.Models.ServiceModels.Comment.Input
{
    using Microsoft.AspNetCore.Identity;

    public class AddCommentInputModel
    {
        public string Body { get; set; }
        public IdentityUser Author { get; set; }
        public int AdvertisementId { get; set; }
    }
}
