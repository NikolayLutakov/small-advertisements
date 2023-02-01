using System.ComponentModel.DataAnnotations;

namespace SmallAdvertisements.Models.ViewModels.Users
{
    public class InputFormModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
