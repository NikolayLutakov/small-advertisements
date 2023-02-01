namespace SmallAdvertisements.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SmallAdvertisements.Models.ServiceModels.Advertisement.Input;
    using SmallAdvertisements.Models.ViewModels.Advertisement;
    using SmallAdvertisements.Services.Contracts;

    [Authorize]
    public class AdvertisementsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAdvertisementService _advertisementService;

        public AdvertisementsController(UserManager<IdentityUser> userManager, IAdvertisementService advertisementService)
        {
            _userManager = userManager;
            _advertisementService = advertisementService;
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdvertisementFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            };

            var currentUser = await _userManager.GetUserAsync(User);

            var inputModel = new AddAdvertisementInputModel()
            {
                Body = model.Body,
                Title = model.Title,
                Author = currentUser
            };

            var isSuccesfullyAdded = _advertisementService.Add(inputModel);

            if (!isSuccesfullyAdded)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(int Id)
        {
            var currentAdvertisement = _advertisementService.GetById(Id);

            if (currentAdvertisement == null)
            {
                return BadRequest();
            }

            var formModel = new AdvertisementFormModel
            {
                Id = currentAdvertisement.Id,
                Body = currentAdvertisement.Body,
                Title = currentAdvertisement.Title
            };

            return View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdvertisementFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            };

            var currentUser = await _userManager.GetUserAsync(User);

            var inputModel = new EditAdvertisementInputModel()
            {
                Id = model.Id.Value,
                Body = model.Body,
                Title = model.Title,
                Editor = currentUser
            };

            var isSuccesfullyEdited = _advertisementService.Edit(inputModel);

            if (!isSuccesfullyEdited)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Delete(int Id)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var isSucessfullyDeleted = _advertisementService.Delete(Id, currentUser.Id);

            if (!isSucessfullyDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> MyAdvertisements()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var advertisements = _advertisementService.GetByUser(currentUser.Id);

            var model = new MyAdvertisementsViewModel()
            {
                MyAdvertisements = advertisements
            };

            return View(model);
        }
    }
}