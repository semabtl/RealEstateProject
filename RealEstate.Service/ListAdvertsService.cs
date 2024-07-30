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
    public class ListAdvertsService : IListAdvertsService
    {
        private readonly RealEstateContext _context;

        public ListAdvertsService(RealEstateContext context)
        {
            _context = context;
        }

       public IEnumerable<ListAdvertsModel> FindAdvertsByCity(string cityName)
       {
            var result = from advert in _context.Adverts
                         join address in _context.Addresses on advert.AddressID equals address.AddressID
                         join city in _context.Cities on address.CityID equals city.CityID
                         join district in _context.Districts on address.DistrictID equals district.DistrictID
                         where city.CityName == cityName
                         select new ListAdvertsModel
                         {
                             Title = advert.Title,
                             Description = advert.Description,
                             ListingType = advert.ListingType,
                             PropertyType = advert.PropertyType,
                             SquareMeters = advert.SquareMeters,
                             Price = advert.Price,
                             CityName = cityName,
                             Street = address.StreetName,
                             BuildingNumber = address.BuildingNumber,
                             DoorNumber = address.DoorNumber,
                             Country = address.Country,
                             DistrictName = district.DistrictName,
                         };
            return result.ToList();

        }
       
    
    }
}
