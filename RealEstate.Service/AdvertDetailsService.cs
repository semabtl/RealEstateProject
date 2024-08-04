using RealEstate.DataAccess.Context;
using RealEstate.DataAccess.Models;
using RealEstate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public class AdvertDetailsService : IAdvertDetailsService
    {
        private readonly RealEstateContext _context;

        public AdvertDetailsService(RealEstateContext context)
        {
            _context = context;
        }

        public AdvertDetailsModel GetAdvertDetailsByID(int advertID)
        {
            AdvertDetailsModel? result = new AdvertDetailsModel();

            try
            {
                result = (from advert in _context.Adverts
                              join person in _context.Persons on advert.PersonID equals person.PersonID
                              join address in _context.Addresses on advert.AddressID equals address.AddressID
                              join city in _context.Cities on address.CityID equals city.CityID
                              join district in _context.Districts on address.DistrictID equals district.DistrictID
                              join company in _context.Companies on person.CompanyID equals company.CompanyID into companyJoin
                              from company in companyJoin.DefaultIfEmpty() // Optional join, company bilgisi olmayabilir
                              where advert.AdvertID == advertID
                              select new AdvertDetailsModel
                              {
                                  Title = advert.Title,
                                  Description = advert.Description,
                                  ListingType = advert.ListingType,
                                  PropertyType = advert.PropertyType,
                                  SquareMeters = advert.SquareMeters,
                                  Price = advert.Price,
                                  CityName = city.CityName,
                                  Street = address.StreetName,
                                  BuildingNumber = address.BuildingNumber,
                                  DoorNumber = address.DoorNumber,
                                  Country = address.Country,
                                  DistrictName = district.DistrictName,
                                  AdvertiserName = person.Name,
                                  AdvertiserSurname = person.Surname,
                                  AdvertiserPhoneNumber = person.PhoneNumber,
                                  AdvertiserCompanyName = company != null ? company.CompanyName : null
                              }).FirstOrDefault();

                //İlana ait görseller ayrıca alınır.
                if (result != null)
                {
                    result.ImagePaths = _context.Images
                                               .Where(img => img.AdvertID == advertID)
                                               .Select(img => img.PathToImage)
                                               .ToList();                   
                }
            }
            catch (Exception)
            {
               
            }

            return result;
        
        }
    }
}
