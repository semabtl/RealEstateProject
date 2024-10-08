﻿using Microsoft.AspNetCore.Mvc;
using RealEstate.DataAccess.Models;
using RealEstate.Service;

namespace RealEstate.Web.Controllers
{
    public class ContactApplicationController : Controller
    {
        private readonly IContactApplicationService _contactApplicationService;

        public ContactApplicationController(IContactApplicationService contactApplicationService)
        {
            _contactApplicationService = contactApplicationService;
        }

        [HttpGet("contact-application")]
        public IActionResult ContactApplication()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;

            return View("~/Views/Shared/ContactApplication.cshtml");
        }

        [HttpGet("company-contact-application")]
        public IActionResult CompanyContactApplication()
        {
            ViewData["ActivePage"] = "CompanyContactApplication";
            return View("~/Views/CompanyContactApplication.cshtml");
        }

        [HttpPost("contact-application")]
        public async Task<IActionResult> ContactApplication(ContactApplicationModel model)
        {           
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;

            if (ModelState.IsValid)
            {
                var result = await _contactApplicationService.AddContactApplication(model);
                if (result.success)
                {
                    ViewBag.SuccessMessage = result.message;
                }
                else
                {
                    ViewBag.ErrorMessage = result.message;
                }

            }
            return View("~/Views/Shared/ContactApplication.cshtml");
        }

        [HttpPost("company-contact-application")]
        public async Task<IActionResult> CompanyContactApplication(CompanyContactApplicationModel model)
        {
            ViewData["ActivePage"] = "CompanyContactApplication";
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;

            if (ModelState.IsValid)
            {
                var result = await _contactApplicationService.AddCompanyContactApplication(model);
                if (result.success)
                {
                    ViewBag.SuccessMessage = result.message;
                }
                else 
                { 
                    ViewBag.ErrorMessage = result.message;
                }
            }
            return View("~/Views/CompanyContactApplication.cshtml", model);
        }

        [HttpGet("get-all-company-contact-applications")]
        public IActionResult ListAllCompanyApplications()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            ViewBag.UserRole = Entity.Role.Admin;

            var applications = _contactApplicationService.ListAllCompanyContactApplications();

            return View("~/Views/ListCompanyContactApplications.cshtml", applications);
        }

        [HttpGet("get-contact-applications")]
        public IActionResult ListAllContactApplications()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;
            ViewBag.UserRole = Entity.Role.Admin;

            var applications = _contactApplicationService.ListAllContactApplications();

            return View("~/Views/ListContactApplications.cshtml", applications);
        }

    }
}
