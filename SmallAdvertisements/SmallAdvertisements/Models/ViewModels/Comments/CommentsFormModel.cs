namespace SmallAdvertisements.Models.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentsFormModel
    {
        public string CallerView { get; set; }

        public string AdvertisementTitle { get; set; }
        public int? Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Body { get; set; }
        public int AdvertisementId { get; set; }
    }
}
