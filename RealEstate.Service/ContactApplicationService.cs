using Microsoft.EntityFrameworkCore;
using RealEstate.DataAccess.Context;
using RealEstate.DataAccess.Migrations;
using RealEstate.DataAccess.Models;
using RealEstate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public class ContactApplicationService: IContactApplicationService
    {
        private readonly RealEstateContext _context;

        public ContactApplicationService(RealEstateContext context)
        {
            _context = context;
        }

        public async Task<(bool success, string message)> AddContactApplication(ContactApplicationModel model)
        {
            try
            {
                var contactApplicationEntity = new ContactApplication
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    PhoneNumber = model.PhoneNumber,
                    MessageText = model.MessageText
                };
                _context.ContactApplications.Add(contactApplicationEntity);
                await _context.SaveChangesAsync();
                return (true, "Kayıt başarıyla tamamlandı.");

            }
            catch (Exception ex)
            {
                return (false, "İlan kaydı yapılamadı.");
            }


        }
        public async Task<(bool success, string message)> AddCompanyContactApplication(CompanyContactApplicationModel model)
        {
            try
            {
                //Girilen ada sahip ilin varlığı kontrol edilir.
                var city = await _context.Cities.FirstOrDefaultAsync(c => c.CityName == model.CityName);
                if (city == null)
                {
                    return (false, "İl adı yanlış.");
                }

                //Girilen ada sahip ilçenin varlığı kontrol edilir.
                var district = await _context.Districts.FirstOrDefaultAsync(d => d.DistrictName == model.DistrictName);
                if (district == null)
                {
                    return (false, "İlçe adı yanlış.");
                }

                var companyContactEntity = new CompanyContactApplication
                {
                    CompanyName = model.CompanyName,
                    PhoneNumber = model.PhoneNumber,
                    CityID = city.CityID,
                    DistrictID = district.DistrictID
                };
                _context.CompanyContactApplications.Add(companyContactEntity);
                await _context.SaveChangesAsync();

                return (true, "Başvuru tamamlandı. En kısa zamanda sizinle iletişime geçeceğiz.");
            }
            catch (Exception ex)
            {
                return (false, "Kayıt İşlemi Yapılamadı.");
            }

        }

        public IEnumerable<CompanyContactApplicationModel> ListAllCompanyContactApplications() 
        {
            try
            {
                var result = from application in _context.CompanyContactApplications
                             join city in _context.Cities on application.CityID equals city.CityID
                             join district in _context.Districts on application.DistrictID equals district.DistrictID
                             where application.IsDeleted == false
                             select new CompanyContactApplicationModel
                             {
                                 CompanyContactApplicationID = application.CompanyContactApplicationID,
                                 CompanyName = application.CompanyName,
                                 PhoneNumber = application.PhoneNumber,
                                 CityName = city.CityName,
                                 DistrictName = district.DistrictName
                             };
                return result.ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        
        public IEnumerable<ContactApplicationModel> ListAllContactApplications()
        {
            try
            {
                var applications = from application in _context.ContactApplications
                                   where application.IsDeleted == false
                                   select new ContactApplicationModel
                                   {
                                       ContactApplicationID = application.ContactApplicationID,
                                       Name = application.Name,
                                       Surname = application.Surname,
                                       PhoneNumber= application.PhoneNumber,
                                       MessageText = application.MessageText
                                   };
                return applications.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
