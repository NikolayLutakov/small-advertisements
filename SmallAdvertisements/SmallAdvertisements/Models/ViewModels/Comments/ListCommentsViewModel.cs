using SmallAdvertisements.Models.ServiceModels.Comment.Output;

namespace SmallAdvertisements.Models.ViewModels.Comments
{
    public class ListCommentsViewModel
    {
        public string AdvertisemetTitle { get; set; }
        public List<CommentOutputModel> Comments { get; set; }
    }
}
