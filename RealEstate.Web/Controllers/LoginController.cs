using Microsoft.AspNetCore.Mvc;
using RealEstate.Service;
using RealEstate.Web.Models;

namespace RealEstate.Web.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }


        /*public IActionResult Index()
        {
            return View();
        }*/
        [HttpGet("")]
        public IActionResult Login()
        {
            ViewData["ActivePage"] = "Login";
            return View("~/Views/Login.cshtml");
        }

        [HttpPost("")]
        public IActionResult Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                bool isValid = _loginService.CheckPerson(model.Email, model.Password);

                if (isValid)
                {
                    HttpContext.Session.SetString("UserEmail", model.Email);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("~/Views/Login.cshtml", model);

        }
    }
}
