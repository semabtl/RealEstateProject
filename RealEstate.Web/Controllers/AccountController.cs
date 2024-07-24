using Microsoft.AspNetCore.Mvc;
using RealEstate.DataAccess.Models;

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
            HttpContext.Session.Remove("UserEmail");

            return RedirectToAction("Login");
        }

        [HttpGet("register/registering-selection")]
        public IActionResult RegisteringSelection()
        {
            return View("~/Views/RegisteringSelection.cshtml");
        }

        [HttpGet("register/personal-register")]
        public IActionResult PersonalRegister()
        {
            
            return View("~/Views/PersonalRegister.cshtml");
        }

        [HttpPost("register/personal-register")]
        public IActionResult PersonalRegister(PersonalRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                bool isValid = true;

                if (isValid)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            //Kayıt işlemi yapılamadıysa
            return View("~/Views/PersonalRegister.cshtml", model);
        } 
    }
}
