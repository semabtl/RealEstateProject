using Microsoft.AspNetCore.Mvc;
using RealEstate.Service;
using RealEstate.DataAccess.Models;
using System.Diagnostics;


namespace RealEstate.Web.Controllers
{
    public class HomeController : Controller
    {
        /*  private readonly ILogger<HomeController> _logger;
          private readonly ILoginService _loginService;

          public HomeController(ILoginService loginService)
          {
              _loginService = loginService;
          }
         */
        private readonly IAllCitiesService _allCitiesService;

        public HomeController(IAllCitiesService allCitiesService)
        {
            _allCitiesService = allCitiesService;
        }

        public IActionResult Index()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            ViewData["ActivePage"] = "Home";

            var cities = _allCitiesService.GetAllCities();

            return View(cities);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        
    }
}