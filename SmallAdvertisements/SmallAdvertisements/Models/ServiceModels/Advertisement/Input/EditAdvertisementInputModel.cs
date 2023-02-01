namespace SmallAdvertisements.Models.ServiceModels.Advertisement.Input
{
    using Microsoft.AspNetCore.Identity;

    public class EditAdvertisementInputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public IdentityUser Editor { get; set; }

    }
}
