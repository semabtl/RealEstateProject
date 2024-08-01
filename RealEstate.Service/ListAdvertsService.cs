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
                         join paidAdvert in _context.PaidAdverts on advert.AdvertID equals paidAdvert.AdvertID into paidAdvertJoin
                         from paidAdvert in paidAdvertJoin.DefaultIfEmpty()
                         join paidAdvertPrice in _context.PaidAdvertPrices on paidAdvert.PaidTypeID equals paidAdvertPrice.ID into paidAdvertPriceJoin
                         from paidAdvertPrice in paidAdvertPriceJoin.DefaultIfEmpty()
                         where city.CityName == cityName
                         select new ListAdvertsModel
                         {
                             AdvertID = advert.AdvertID,
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
                             PaidAdvertChoice = paidAdvertPrice != null ? paidAdvertPrice.Title : null
                         };
            return result.ToList();

        }
        public ListAdvertsModel GetAdvertByID(int advertID)
        {
            var result = (from advert in _context.Adverts
                         join address in _context.Addresses on advert.AddressID equals address.AddressID
                         join city in _context.Cities on address.CityID equals city.CityID
                         join district in _context.Districts on address.DistrictID equals district.DistrictID
                         where advert.AdvertID == advertID
                         select new ListAdvertsModel
                         {
                             AdvertID = advert.AdvertID,
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
                         }).FirstOrDefault();
            return result;
        }
       
    
    }
}
