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

       public IEnumerable<ListAdvertsModel> FindAdvertsByCity(string userEmail, string cityName)
       {
            var person = _context.Persons.FirstOrDefault(p => p.Email == userEmail);
            int? personID;
            if(person == null)
            {
                personID = null;
            }
            else{
                personID = person.PersonID;
            }
            
            var result = from advert in _context.Adverts
                         join address in _context.Addresses on advert.AddressID equals address.AddressID
                         join city in _context.Cities on address.CityID equals city.CityID
                         join district in _context.Districts on address.DistrictID equals district.DistrictID
                         join paidAdvert in _context.PaidAdverts on advert.AdvertID equals paidAdvert.AdvertID into paidAdvertJoin
                         from paidAdvert in paidAdvertJoin.DefaultIfEmpty()
                         join paidAdvertPrice in _context.PaidAdvertPrices on paidAdvert.PaidTypeID equals paidAdvertPrice.ID into paidAdvertPriceJoin
                         from paidAdvertPrice in paidAdvertPriceJoin.DefaultIfEmpty()
                         where city.CityName == cityName
                         where advert.IsDeleted == false
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
                             PaidAdvertChoice = paidAdvertPrice != null ? paidAdvertPrice.Title : null,
                             Status = advert.Status,
                             IsFavourite = personID.HasValue && _context.Favourites.Any(f => (f.AdvertID == advert.AdvertID) && (f.PersonID == personID) && (f.IsDeleted == false))
                         };

            var adverts = result.ToList();

            //İlana ait görsel kaydı varsa, listede görüntülenmek üzere ilk görselin dosya yolu alınır.
            foreach (var advert in adverts)
            {
                var firstImagePath = _context.Images
                                           .Where(img => img.AdvertID == advert.AdvertID)
                                           .OrderBy(img => img.ImageID)
                                           .Select(img => img.PathToImage)
                                           .FirstOrDefault();

                advert.PathToImage = firstImagePath;
            }

            

            return adverts;

        }
        public IEnumerable<ListAdvertsModel> ListAllAdverts()
        {
            try
            {
                var result = from advert in _context.Adverts
                             join address in _context.Addresses on advert.AddressID equals address.AddressID
                             join city in _context.Cities on address.CityID equals city.CityID
                             join district in _context.Districts on address.DistrictID equals district.DistrictID
                             join paidAdvert in _context.PaidAdverts on advert.AdvertID equals paidAdvert.AdvertID into paidAdvertJoin
                             from paidAdvert in paidAdvertJoin.DefaultIfEmpty()
                             join paidAdvertPrice in _context.PaidAdvertPrices on paidAdvert.PaidTypeID equals paidAdvertPrice.ID into paidAdvertPriceJoin
                             from paidAdvertPrice in paidAdvertPriceJoin.DefaultIfEmpty()
                             where advert.IsDeleted == false
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
                                 PaidAdvertChoice = paidAdvertPrice != null ? paidAdvertPrice.Title : null,
                                 Status = advert.Status,
                                 IsFavourite = false
                             };

                var adverts = result.ToList();

                foreach (var advert in adverts)
                {
                    var firstImagePath = _context.Images
                                               .Where(img => img.AdvertID == advert.AdvertID)
                                               .OrderBy(img => img.ImageID)
                                               .Select(img => img.PathToImage)
                                               .FirstOrDefault();

                    advert.PathToImage = firstImagePath;
                }

                return adverts;
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }
            /*  public ListAdvertsModel GetAdvertByID(int advertID)
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
             */

        }
}
