using Microsoft.AspNetCore.Mvc;
using RealEstate.DataAccess.Models;
using RealEstate.Service;

namespace RealEstate.Web.Controllers
{
    public class AddNewArticleController : Controller
    {
        private readonly IAddNewArticleService _addNewArticleService;

        public AddNewArticleController(IAddNewArticleService addNewArticleService)
        {
            _addNewArticleService = addNewArticleService;
        }

        [HttpGet("news-options-selection")]
        public IActionResult NewsOptions()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            ViewBag.UserRole = Entity.Role.Admin;

            return View("~/Views/NewsOptions.cshtml");
        }

        [HttpGet("add-new-article")]
        public IActionResult AddNewArticle()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            ViewBag.UserRole = Entity.Role.Admin;

            return View("~/Views/AddNewArticle.cshtml");
        }

        [HttpPost("add-new-article")]
        public IActionResult AddNewArticle(ArticleModel model)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            ViewBag.UserRole = Entity.Role.Admin;

            model.AuthorEmail = userEmail;
            //service metodunu çağır. model email set et
            //photo upload
            var succeed = _addNewArticleService.AddNewArticle(model);
            if (succeed)
            {
                TempData["SuccessMessage"] = "Haber yüklendi.";
            }
            else
            {
                TempData["ErrorMessage"] = "Haber yüklenemedi.";
            }
            return View("~/Views/AddNewArticle.cshtml", model);
        }
    }
}
