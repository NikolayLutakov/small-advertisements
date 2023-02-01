namespace SmallAdvertisements.Models.ServiceModels.Advertisement.Input
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class AddAdvertisementInputModel
    {
        public IdentityUser Author { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }


    }
}
