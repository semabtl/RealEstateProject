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

        [HttpGet("corporate-register")]
        public IActionResult CorporateRegister()
        {

            return View("~/Views/CorporateRegister.cshtml");
        }

        [HttpPost("personal-register")]
        public async Task<IActionResult> PersonalRegister(PersonalRegisterModel model)
        {
            if (ModelState.IsValid)
            {

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

        [HttpPost("corporate-register")]
        public async Task<IActionResult> CorporateRegister(CorporateRegisterModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _registerService.AddCorporateAccountAsync(model);
                if (result.success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = result.message;
                }
            }
            return View("~/Views/CorporateRegister.cshtml", model);

        }
    }
}
