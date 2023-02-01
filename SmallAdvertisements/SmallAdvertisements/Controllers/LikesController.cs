using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmallAdvertisements.Services.Contracts;

namespace SmallAdvertisements.Controllers
{
    [Authorize]
    public class LikesController : Controller
    {
        private readonly ILikeService _likeService;
        private readonly UserManager<IdentityUser> _userManager;

        public LikesController(ILikeService likeService, UserManager<IdentityUser> userManager)
        {
            _likeService = likeService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Like(int advertisementId, string caller)
        {
            var controller = "Advertisements";
            var action = "MyAdvertisements";
            if(!string.IsNullOrEmpty(caller))
            {
                controller = "Home";
                action = "Index";
            }

            var currentUser = await _userManager.GetUserAsync(User);

            _likeService.Like(advertisementId, currentUser);

            return RedirectToAction(action, controller);
        }

        public async Task<IActionResult> Unlike(int advertisementId, string caller)
        {
            var controller = "Advertisements";
            var action = "MyAdvertisements";
            if (!string.IsNullOrEmpty(caller))
            {
                controller = "Home";
                action = "Index";
            }

            var currentUser = await _userManager.GetUserAsync(User);

            _likeService.Unlike(advertisementId, currentUser);


            return RedirectToAction(action, controller);
        }
    }
}
