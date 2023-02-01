namespace SmallAdvertisements.Models.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentsFormModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Body { get; set; }
        public int AdvertisementId { get; set; }
    }
}
