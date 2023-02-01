using Microsoft.AspNetCore.Identity;

namespace SmallAdvertisements.Services.Contracts
{
    public interface ILikeService
    {
        bool Like(int advertisementId, IdentityUser user);

        bool Unlike(int advertisementId, IdentityUser user);
    }
}
