using Microsoft.AspNetCore.Identity;

namespace SmallAdvertisements.Data.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
