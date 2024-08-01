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
        private readonly IPaidAdvertsHomepageService _paidAdvertsHomepageService;
        public HomeController(IAllCitiesService allCitiesService, IPaidAdvertsHomepageService paidAdvertsHomepageService)
        {
            _allCitiesService = allCitiesService;
            _paidAdvertsHomepageService = paidAdvertsHomepageService;
        }

        public IActionResult Index()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            ViewData["ActivePage"] = "Home";

            HomepageViewModel model = new HomepageViewModel();
            model.AllCities = _allCitiesService.GetAllCities();
            model.PaidHomepageAdverts = _paidAdvertsHomepageService.GetFeaturedAdverts();


           // var cities = _allCitiesService.GetAllCities();

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        
    }
}