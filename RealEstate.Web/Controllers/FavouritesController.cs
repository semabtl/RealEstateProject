using Microsoft.AspNetCore.Mvc;
using RealEstate.DataAccess.Models;
using RealEstate.Service;

namespace RealEstate.Web.Controllers
{
    public class FavouritesController : Controller
    {
        private readonly IFavouritesService _favouritesService;
        public FavouritesController(IFavouritesService favouritesService)
        {
            _favouritesService = favouritesService;
        }

        [HttpPost("add-to-favourites")]
        public async Task<IActionResult> AddToFavourites(FavouritesModel model, string cityName)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            model.UserEmail = userEmail;

            if(userEmail == null)
            {
                return RedirectToAction("Login","Account");
            }
            var succeed = await _favouritesService.AddNewFavourite(model);
            if (succeed)
            {
                TempData["SuccessMessage"] = "Favorilere eklendi!";
            }
            else
            {
                TempData["ErrorMessage"] = "Favorilere eklenemedi.";
            }
            return RedirectToAction("ListAdverts", "ListAdverts", new { cityName = cityName });
           
        }

        [HttpGet("get-all-favourites")]
        public IActionResult GetAllFavourites(FavouritesModel model)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;

            model.UserEmail = userEmail;

            TempData["ActivePage"] = "FavouritesOfAPerson";

            var result = _favouritesService.GetAllFavourites(model);
            if (result == null)
            {
                ViewBag.ErrorMessage = "Favori ilan bulunmamaktadır."; 
            }
            
            return View("~/Views/FavouritesOfAPerson.cshtml", result);
            
        }

        [HttpPost("remove-from-favourites")]
        public async Task<IActionResult> RemoveFromFavourites(int advertID, string cityName)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;

            FavouritesModel model = new FavouritesModel();
            model.UserEmail = userEmail;
            model.AdvertID = advertID;
            model.CityName = cityName;

            var succeed = await _favouritesService.RemoveFromFavourites(model); 
            if (succeed) 
            {
                TempData["SuccessMessage"] = "Seçilen ilan favorilerden kaldırıldı.";
            }
            else
            {
                TempData["ErrorMessage"] = "İlan favorilerden kaldırılamadı.";
            }

            if ((string)TempData["ActivePage"] == "ListAdverts")
            {
                return RedirectToAction("ListAdverts", "ListAdverts", new { cityName = cityName });
            }
            else if((string)TempData["ActivePage"] == "FavouritesOfAPerson")
            {
                return RedirectToAction("GetAllFavourites", "Favourites", model);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
