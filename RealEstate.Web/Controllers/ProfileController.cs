using Microsoft.AspNetCore.Mvc;
using RealEstate.DataAccess.Models;
using RealEstate.Service;

namespace RealEstate.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IListUsersService _usersService;
        private readonly IListAdvertsService _advertsService;
        private readonly IDeletionService _deletionService;
        public ProfileController(IListUsersService usersService, IListAdvertsService advertsService, IDeletionService deletionService)
        {
            _usersService = usersService;
            _advertsService = advertsService;
            _deletionService = deletionService;
        }

        [HttpGet("profile-information")]
        public IActionResult GetProfileInformation()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;

            ProfileViewModel model = new ProfileViewModel();

            if (userEmail == null)
            {
                TempData["ErrorMessage"] = "Kullanıcı bilgilerine ulaşılamadı. Lütfen daha sonra tekrar deneyin.";
            } 
            else
            {
                model.UserInformation = _usersService.GetUserInformationByEmail(userEmail);
                model.AdvertsOfUser = _advertsService.FindAdvertsOfUser(userEmail);
            }
            

            return View("~/Views/Profile.cshtml", model);
        }

        [HttpPost("delete-advert")]
        public IActionResult DeleteAdvert(int advertID)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;

            var succeed = _deletionService.DeleteAnAdvert(advertID);
            if (succeed)
            {
                TempData["SuccessMessage"] = "İlan silindi.";
            }
            else
            {
                TempData["ErrorMessage"] = "İlan silinemedi.";
            }
            return RedirectToAction("GetProfileInformation", "Profile");
        }
    }
}
