using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmallAdvertisements.Data.Entities;

namespace SmallAdvertisements.Data.Context
{
    public class AdvertisementsDbContext : IdentityDbContext<IdentityUser>
    {
        public AdvertisementsDbContext(DbContextOptions<AdvertisementsDbContext> options) : base(options)
        {

        }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }

       


    }
}
