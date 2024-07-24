using Microsoft.AspNetCore.Mvc;
using RealEstate.DataAccess.Models;
using RealEstate.Service;

namespace RealEstate.Web.Controllers
{
    [Route("register")]
    public class RegisterController : Controller
    {
        private readonly IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }
        [HttpGet("registering-selection")]
        public IActionResult RegisteringSelection()
        {
            return View("~/Views/RegisteringSelection.cshtml");
        }

        [HttpGet("personal-register")]
        public IActionResult PersonalRegister()
        {

            return View("~/Views/PersonalRegister.cshtml");
        }

        /* [HttpPost("register/personal-register")]
         public IActionResult PersonalRegister(PersonalRegisterModel model)
         {
             if (ModelState.IsValid)
             {
                 //DEĞİŞTİRİLECEK
                 bool isValid = true;

                 if (isValid)
                 {
                     return RedirectToAction("Index", "Home");
                 }
             }
             //Kayıt işlemi yapılamadıysa
             return View("~/Views/PersonalRegister.cshtml", model);
         } */

        [HttpPost("personal-register")]
        public async Task<IActionResult> PersonalRegister(PersonalRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                /* bool isSuccess = await _registerService.AddPersonAsync(model);
                 if (isSuccess)
                 {
                     return RedirectToAction("Index", "Home");
                 }
                 else
                 {
                     ViewBag.ErrorMessage = "Kayıt işlemi yapılamadı.";
                 }*/

                var result = await _registerService.AddPersonAsync(model);
                if (result.success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = result.message;
                }

            }
               
            return View("~/Views/PersonalRegister.cshtml", model);
        }
    }
}
