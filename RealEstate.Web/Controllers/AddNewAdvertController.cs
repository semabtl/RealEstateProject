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
                model.PhotoPaths = SavePhotos(model.Files);

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
                }
                else
                {
                    ViewBag.ErrorMessage = result.message;
                }
            }
            
            return View("~/Views/AddNewAdvert.cshtml");
        }


        //Fotoğraf kaydı için metot
        public List<string> SavePhotos(List<IFormFile> files)
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var photoPaths = new List<string>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    //Fotoğraf adı için unique değer oluşturulur.
                    Guid guid = Guid.NewGuid();
                    var filePath = Path.Combine(uploadPath, guid.ToString() + ".png");

                    // Fotoğraf kaydedilir.
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyToAsync(stream);
                    }
                    
                    //Fotoğrafın yolu listeye eklenir.
                    photoPaths.Add($"~/uploads/"+ guid.ToString() + ".png");
                }
            }

            return photoPaths;
        }
    }
}
