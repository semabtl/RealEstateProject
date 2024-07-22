using Microsoft.AspNetCore.Mvc;
using RealEstate.Web.Models;

namespace RealEstate.Web.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
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
                bool isValid = true;

                if (isValid)
                {
                    HttpContext.Session.SetString("UserEmail", model.Email);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("~/Views/Login.cshtml", model);

        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Oturumdan e-posta adresini kaldırarak çıkış yap
            HttpContext.Session.Remove("UserEmail");

            // Giriş sayfasına yönlendir
            return RedirectToAction("Login");
        }
    }
}
