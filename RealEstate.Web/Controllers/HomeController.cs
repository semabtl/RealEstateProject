using Microsoft.AspNetCore.Mvc;
using RealEstate.Service;
using RealEstate.Web.Models;
using System.Diagnostics;

namespace RealEstate.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoginService _loginService;

        public HomeController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            ViewData["ActivePage"] = "Home";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /*[HttpPost]
        public IActionResult Login([FromForm] LoginCredentialModel loginModel)
        {
            var result = _loginService.Detail(loginModel.Email, loginModel.Password);

            return View();
        }*/
    }
}