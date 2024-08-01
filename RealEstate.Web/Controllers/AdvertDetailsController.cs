using Microsoft.AspNetCore.Mvc;
using RealEstate.Service;

namespace RealEstate.Web.Controllers
{
    public class AdvertDetailsController : Controller
    {
        private readonly IAdvertDetailsService _advertDetailsService;

        public AdvertDetailsController(IAdvertDetailsService advertDetailsService)
        {
            _advertDetailsService = advertDetailsService;
        }

        [HttpGet("get-advert-by-id")]
        public IActionResult GetAdvert(int advertID)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;

            var result = _advertDetailsService.GetAdvertDetailsByID(advertID);

            return View("~/Views/AdvertDetails.cshtml", result);
           

        }
    }
}
