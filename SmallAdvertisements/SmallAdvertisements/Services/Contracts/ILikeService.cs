using Microsoft.AspNetCore.Identity;
using SmallAdvertisements.Models.ServiceModels.Like.Output;

namespace SmallAdvertisements.Services.Contracts
{
    public interface ILikeService
    {
        bool Like(int advertisementId, IdentityUser user);

        bool Unlike(int advertisementId, IdentityUser user);

        ICollection<LikeOutputModel> GetUserLikes(string userId);
    }
}
