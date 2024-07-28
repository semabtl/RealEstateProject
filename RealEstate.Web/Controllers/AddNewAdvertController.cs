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
                if (!(model.PaidAdvertChoice.Equals("Ücretsiz İlan")))
                {
                    return View("~/Views/PaymentPage.cshtml", model);
                }

                var result = await _addAdvertService.AddAdvertAsync(model, userEmail);
                if (result.success)
                {
                    ViewBag.SuccessMessage = result.message;
                }
                else
                {
                    ViewBag.ErrorMessage = result.message;
                }

            }
            return View("~/Views/AddNewAdvert.cshtml", model);
        }

        [HttpPost("advertise-paid")]
        public async Task<IActionResult> AddNewPaidAdvert(AddNewAdvertModel model)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;

            if(ModelState.IsValid)
            {
                var advertModel = new AddNewAdvertModel
                {
                    Title = Request.Form["Title"],
                    Description = Request.Form["Description"],
                    ListingType = Request.Form["ListingType"],
                    PropertyType = Request.Form["PropertyType"],
                    SquareMeters = Request.Form["SquareMeters"],
                    Price = Request.Form["Price"],
                    CityName = Request.Form["CityName"],
                    Street = Request.Form["Street"],
                    BuildingNumber = Request.Form["BuildingNumber"],
                    DoorNumber = Request.Form["DoorNumber"],
                    DistrictName = Request.Form["DistrictName"],
                    PaidAdvertChoice = Request.Form["PaidAdvertChoice"]
                };

                var result1 = await _addAdvertService.AddAdvertAsync(model, userEmail);
                var result2 = await _addAdvertService.AddPaidAdvertAsync(userEmail, result1.advertID);

                if (result1.success && result2.success)
                {
                    ViewBag.SuccessMessage = result1.message;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Hata, İlan Kaydedilemedi";
                }
            }
            
            return View("~/Views/AddNewAdvert.cshtml", model);
        }
    }
}
