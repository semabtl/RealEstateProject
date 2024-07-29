using Microsoft.AspNetCore.Mvc;
using RealEstate.DataAccess.Models;
using RealEstate.Service;

namespace RealEstate.Web.Controllers
{

    public class AddNewAdvertController : Controller
    {
        private readonly IAddNewAdvertService _addAdvertService;

        public AddNewAdvertController(IAddNewAdvertService addAdvertService)
        {
            _addAdvertService = addAdvertService;
        }
        /* public IActionResult Index()
         {
             return View();
         }*/
        [HttpGet("advertise")]
        public IActionResult AddNewAdvert()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            ViewData["ActivePage"] = "AddNewAdvert";
            return View("~/Views/AddNewAdvert.cshtml");
        }

        [HttpPost("advertise")]
        public async Task<IActionResult> AddNewAdvert(AddNewAdvertModel model)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;

            if (ModelState.IsValid)
            {
                
                var result = await _addAdvertService.AddAdvertAsync(model, userEmail);
                if (result.success)
                {
                    ViewBag.SuccessMessage = result.message;
                }
                else
                {
                    ViewBag.ErrorMessage = result.message;
                }
                if (!(model.PaidAdvertChoice.Equals("Ücretsiz İlan")))
                {
                    return View("~/Views/PaymentPage.cshtml", model);
                }

            }
            return View("~/Views/AddNewAdvert.cshtml", model);
        }

        [HttpPost("advertise-paid")]
        public async Task<IActionResult> AddNewPaidAdvert()
        {
            var paidAdvertChoice = Request.Form["PaidAdvertChoice"];
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;

            if(ModelState.IsValid)
            {            
                var result = await _addAdvertService.AddPaidAdvertAsync(paidAdvertChoice, userEmail);

                if (result.success)
                {
                    ViewBag.SuccessMessage = result.message;
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = result.message;
                }
            }
            
            return View("~/Views/AddNewAdvert.cshtml");
        }
    }
}
