using Microsoft.AspNetCore.Mvc;
using RealEstate.DataAccess.Models;
using RealEstate.Service;

namespace RealEstate.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("news-options-selection")]
        public IActionResult NewsOptions()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            ViewBag.UserRole = Entity.Role.Admin;
            //TempData["UserRole"] = "Admin";
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
        public IActionResult AddNewArticle(NewsModel model)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            ViewBag.UserRole = Entity.Role.Admin;

            model.AuthorEmail = userEmail;
            
            var succeed = _newsService.AddNewArticle(model);
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

        [HttpGet("list-all-news")]
        public IActionResult ListAllNews()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            
            var allNews = _newsService.GetAllNews();

            /* TempData da ViewBag de null geldiği için ayrı metoda taşındı.
             * if ((string)TempData["UserRole"]  == "Admin")
            {
                return View("~/Views/ListAllNewsForAdmin.cshtml", allNews);
            }*/
            return View("~/Views/ListAllNews.cshtml", allNews);
            
        }
        [HttpGet("list-all-news-for-admin")]
        public IActionResult ListAllNewsForAdmin()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            ViewBag.UserRole = Entity.Role.Admin;

            var allNews = _newsService.GetAllNews();

            return View("~/Views/ListAllNewsForAdmin.cshtml", allNews);

        }

        [HttpGet("news-details")]
        public IActionResult GetAnArticle(int newsID) 
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
           

            var article = _newsService.GetAnArticle(newsID);

            return View("~/Views/NewsDetails.cshtml", article);

        }
    }
}
