using Microsoft.AspNetCore.Mvc;
using RealEstate.DataAccess.Models;
using RealEstate.Service;

namespace RealEstate.Web.Controllers
{
    public class ListAdvertsController : Controller
    {    
        private readonly IListAdvertsService _listAdvertsService;

        public ListAdvertsController(IListAdvertsService listAdvertsService)
        {
            _listAdvertsService = listAdvertsService;
        }

        [HttpGet("list-adverts-by-city")]
        public IActionResult ListAdverts(string cityName)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;

            ViewBag.CityName = cityName;

            TempData["ActivePage"] = "ListAdverts";

            var result = _listAdvertsService.FindAdvertsByCity(userEmail, cityName);

            return View("~/Views/ListAdverts.cshtml", result);
        
        }
        
    }
}
