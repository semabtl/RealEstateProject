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
            if (ModelState.IsValid)
            {
                var result = await _addAdvertService.AddAdvertAsync(model, ViewBag.UserEmail);
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

     }
}
