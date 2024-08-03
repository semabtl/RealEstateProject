using Microsoft.AspNetCore.Mvc;
using RealEstate.Service;

namespace RealEstate.Web.Controllers
{
    public class AddToFavouritesController : Controller
    {
        private readonly IAddToFavouritesService _addToFavouritesService;
        public AddToFavouritesController(IAddToFavouritesService addToFavouritesService)
        {
            _addToFavouritesService = addToFavouritesService;
        }

        [HttpPost("add-to-favourites")]
        public async Task<IActionResult> AddToFavourites(int advertID, string cityName)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;

            if(userEmail == null)
            {
                return RedirectToAction("Login","Account");
            }
            var succeed = await _addToFavouritesService.AddNewFavourite(userEmail, advertID);
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
    }
}
