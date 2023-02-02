namespace SmallAdvertisements.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SmallAdvertisements.Data.Entities;
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

        public ActionResult Add(int advertisementId, string adTitle, string callerView)
        {
            var formModel = new CommentsFormModel
            {
                CallerView = callerView,
                AdvertisementTitle = adTitle,
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

            if (model.CallerView == "Details")
            {
                return RedirectToAction("Details", "Advertisements", new { advertisementId = model.AdvertisementId });
            }

            if (model.CallerView == "MyAdvertisements")
            {
                return RedirectToAction("MyAdvertisements", "Advertisements");
            }

            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public ActionResult Edit(int commentId, string callerView)
        {
            var comment = _commentService.GetById(commentId);

            var formModel = new CommentsFormModel();

            if(comment != null)
            {
                formModel.CallerView = callerView;
          
                formModel.AdvertisementTitle = comment.AdvertisementTitle;
                formModel.AdvertisementId = comment.AdvertisementId;
                formModel.Id = comment.Id;
                formModel.Body = comment.Body;
            };
            return View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CommentsFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            
            var inputModel = new EditCommentInputModel()
            {
                Id = model.Id.Value,
                Body = model.Body,
                Editor = currentUser
            };

            var isSuccesfullyAdded = _commentService.Edit(inputModel);

            if (!isSuccesfullyAdded)
            {
                return BadRequest();
            }

            if (model.CallerView == "MyComments")
            {
                return RedirectToAction("MyComments", "Comments");
            }

            if (model.CallerView == "SeeComments")
            {
                return RedirectToAction("SeeComments", "Comments", new { advertisementId = model.AdvertisementId, advertisementTitle = model.AdvertisementTitle });
            }

            if (model.CallerView == "Details")
            {
                return RedirectToAction("Details", "Advertisements", new { advertisementId = model.AdvertisementId });
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult SeeComments(int advertisementId, string advertisementTitle)
        {
            var comments = _commentService.GetCommentsByAdverisement(advertisementId);

            var model = new ListCommentsViewModel()
            {
                AdvertisemetTitle = advertisementTitle,
                Comments = comments.ToList()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int commentId, string callerView)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var comment = _commentService.GetById(commentId);

            var success = _commentService.Delete(commentId, currentUser.Id);

            if (!success)
            {
                return BadRequest();
            }

            if(callerView == "MyComments")
            {
                return RedirectToAction("MyComments", "Comments");
            }

            if (callerView == "SeeComments")
            {
                return RedirectToAction("SeeComments", "Comments", new { advertisementId = comment.AdvertisementId, advertisementTitle = comment.AdvertisementTitle });
            }

            if (callerView == "Details")
            {
                return RedirectToAction("Details", "Advertisements", new { advertisementId = comment.AdvertisementId });
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> MyComments()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var comments = _commentService.GetCommentsByUser(currentUser.Id);

            return View(comments);
        }
    }
}
