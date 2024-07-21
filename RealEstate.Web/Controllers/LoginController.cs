using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Web.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        /*public IActionResult Index()
        {
            return View();
        }*/
        [HttpGet("")]
        public IActionResult Login() 
        {
            ViewData["ActivePage"] = "Login";
            return View("~/Views/LoginPage.cshtml");
        }
    }
}
