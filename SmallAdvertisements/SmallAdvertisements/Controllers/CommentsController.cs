namespace SmallAdvertisements.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SmallAdvertisements.Data.Context;
    using SmallAdvertisements.Models.ServiceModels.Comment.Input;
    using SmallAdvertisements.Models.ViewModels.Comments;
    using SmallAdvertisements.Services.Contracts;

    [Authorize]
    public class CommentsController:Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICommentService _commentService;

        public CommentsController(UserManager<IdentityUser> userManager,ICommentService commentService)
        {
            _userManager=userManager;
            _commentService=commentService;
        }

        public ActionResult Add(int advertisementId)
        {
            var formModel = new CommentsFormModel
            {
                AdvertisementId = advertisementId
            };
            return View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CommentsFormModel model)
        {

            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUser = await _userManager.GetUserAsync(User);

            var inputModel = new AddCommentInputModel()
            {
                AdvertisementId = model.AdvertisementId,
                Body = model.Body,
                Author = currentUser

            };

            var isSuccesfullyAdded = _commentService.Add(inputModel);

            if(!isSuccesfullyAdded)
            {
                return BadRequest();
            }

            return RedirectToAction("Index","Home");


        }









    }
}
