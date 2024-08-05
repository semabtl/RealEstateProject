using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealEstate.DataAccess.Context;
using RealEstate.DataAccess.Models;
using RealEstate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public class FavouritesService : IFavouritesService
    {
        private readonly RealEstateContext _context;

        public FavouritesService(RealEstateContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNewFavourite(FavouritesModel model)
        {
            try
            {
                //E-posta adresine sahip olan kişi veritabanından bulunur. null olursa false dönülür ve ekleme işlemi yapılmaz.
                var person = await _context.Persons.FirstOrDefaultAsync(p => p.Email == model.UserEmail);
                if (person == null)
                {
                    return false;
                }

                //Kişi ID değeri de kullanılarak favoriler tablosuna ekleme yapılır.
                var favouriteEntity = new Favourite
                {
                    AdvertID = model.AdvertID,
                    PersonID = person.PersonID
                };
                _context.Favourites.Add(favouriteEntity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        //Bir kişinin tüm favori ilanlarını bulan metot.
        public IEnumerable<ListAdvertsModel> GetAllFavourites(FavouritesModel favouritesModel)
        {
            try
            {
                var person =  _context.Persons.FirstOrDefault(p => p.Email == favouritesModel.UserEmail);

                if (person == null)
                {
                    return Enumerable.Empty<ListAdvertsModel>();
                }

                var result = from favourite in _context.Favourites
                             join advert in _context.Adverts on favourite.AdvertID equals advert.AdvertID
                             join address in _context.Addresses on advert.AddressID equals address.AddressID
                             join city in _context.Cities on address.CityID equals city.CityID
                             join district in _context.Districts on address.DistrictID equals district.DistrictID
                             join paidAdvert in _context.PaidAdverts on advert.AdvertID equals paidAdvert.AdvertID into paidAdvertJoin
                             from paidAdvert in paidAdvertJoin.DefaultIfEmpty()
                             join paidAdvertPrice in _context.PaidAdvertPrices on paidAdvert.PaidTypeID equals paidAdvertPrice.ID into paidAdvertPriceJoin
                             from paidAdvertPrice in paidAdvertPriceJoin.DefaultIfEmpty()
                             where favourite.PersonID == person.PersonID
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
                                    IsFavourite = true
                             };
                
                var favourites = result.ToList();

                //İlana ait görsel kaydı varsa, listede görüntülenmek üzere ilk görselin dosya yolu alınır.
                foreach (var advert in favourites)
                {
                    var firstImagePath = _context.Images
                                               .Where(img => img.AdvertID == advert.AdvertID)
                                               .OrderBy(img => img.ImageID)
                                               .Select(img => img.PathToImage)
                                               .FirstOrDefault();

                    advert.PathToImage = firstImagePath;
                }

                return favourites;

            }
            catch(Exception ex)
            {
                return Enumerable.Empty<ListAdvertsModel>();
            }          

        }
    }
}
