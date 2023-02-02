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

        public async Task<IActionResult> Like(int advertisementId, string callerView)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var success = _likeService.Like(advertisementId, currentUser);

            if (!success)
            {
                return BadRequest();
            }

            if (callerView == "Details")
            {
                return RedirectToAction("Details", "Advertisements", new { advertisementId = advertisementId });
            }

            if (callerView == "MyAdvertisements")
            {
                return RedirectToAction("MyAdvertisements", "Advertisements");
            }

            if (callerView == "MyLikes")
            {
                return RedirectToAction("MyLikes", "Likes");
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Unlike(int advertisementId, string callerView)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var success = _likeService.Unlike(advertisementId, currentUser);

            if (!success)
            {
                return BadRequest();
            }

            if (callerView == "Details")
            {
                return RedirectToAction("Details", "Advertisements", new { advertisementId = advertisementId });
            }

            if (callerView == "MyAdvertisements")
            {
                return RedirectToAction("MyAdvertisements", "Advertisements");
            }

            if (callerView == "MyLikes")
            {
                return RedirectToAction("MyLikes", "Likes");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> MyLikes()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var likes = _likeService.GetUserLikes(currentUser.Id);

            return View(likes.ToList());
        }
    }
}
