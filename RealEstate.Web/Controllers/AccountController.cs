using Microsoft.AspNetCore.Mvc;
using RealEstate.DataAccess.Models;
using RealEstate.Service;

namespace RealEstate.Web.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly ILoginService _loginService;
        
        public AccountController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        

        [HttpGet("login")]
        public IActionResult Login()
        {
            ViewData["ActivePage"] = "Login";
            return View("~/Views/Login.cshtml");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                bool userFound = _loginService.CheckPerson(model.Email, model.Password);

                if (userFound)
                {
                    HttpContext.Session.SetString("UserEmail", model.Email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Geçersiz e-posta veya şifre. Kullanıcı bulunamadı.";       
                }
            }
            return View("~/Views/Login.cshtml", model);

        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserEmail");

            return RedirectToAction("Login");
        }

        
    }
}
