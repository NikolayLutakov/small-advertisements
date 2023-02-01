namespace SmallAdvertisements.Data.Entities
{
    using Microsoft.AspNetCore.Identity;

    public class Like
    {
        public int Id { get; set; }


        public IdentityUser Author { get; set; }

        public int AdvertisementId { get; set; }

        public Advertisement Advertisement { get; set; }




    }
}
