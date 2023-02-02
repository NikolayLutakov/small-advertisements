namespace SmallAdvertisements.Models.ServiceModels.Comment.Input
{
    using Microsoft.AspNetCore.Identity;

    public class EditCommentInputModel
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public IdentityUser Editor { get; set; }
    }
}
