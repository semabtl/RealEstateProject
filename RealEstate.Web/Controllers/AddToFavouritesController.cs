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
        public async Task<IActionResult> AddToFavourites(int advertID)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;

            var succeed = await _addToFavouritesService.AddNewFavourite(userEmail, advertID);
            if (succeed)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
