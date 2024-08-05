using Microsoft.AspNetCore.Mvc;
using RealEstate.DataAccess.Context;
using RealEstate.Service;

namespace RealEstate.Web.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly IListAdvertsService _listAdvertsService;
        private readonly IDeletionService _deletionService;
        private readonly IListUsersService _listUsersService;
        public AdminController(IListAdvertsService listAdvertsService, IDeletionService deletionService, IListUsersService listUsersService)
        {
            _listAdvertsService = listAdvertsService;
            _deletionService = deletionService;
            _listUsersService = listUsersService;
        }

        [HttpGet("admin-homepage")]
        public IActionResult AdminHomePage()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            ViewBag.UserRole = Entity.Role.Admin;

            return View("~/Views/AdminHomepage.cshtml");
        }

        [HttpGet("list-all-adverts")]
        public IActionResult ListAllAdverts()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            ViewBag.UserRole = Entity.Role.Admin;

            var adverts = _listAdvertsService.ListAllAdverts();

            return View("~/Views/ListAdvertsForAdmin.cshtml", adverts);
        }

        [HttpGet("list-all-users")]
        public IActionResult ListAllUsers()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            ViewBag.UserRole = Entity.Role.Admin;

            var users = _listUsersService.ListAllUsers();

            return View("~/Views/ListAllUsers.cshtml", users);
        }


        [HttpPost("delete-an-advert")]
        public IActionResult DeleteAnAdvert(int advertID)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            ViewBag.UserRole = Entity.Role.Admin;

            var succeed = _deletionService.DeleteAnAdvert(advertID);
            if (succeed)
            {
                TempData["SuccessMessage"] = "İlan silindi.";
            }
            else
            {
                TempData["ErrorMessage"] = "İlan silinemedi.";
            }

            return RedirectToAction("ListAllAdverts", "Admin");
        }
    }
}
