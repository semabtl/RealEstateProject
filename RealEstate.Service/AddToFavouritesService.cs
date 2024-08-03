using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealEstate.DataAccess.Context;
using RealEstate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public class AddToFavouritesService : IAddToFavouritesService
    {
        private readonly RealEstateContext _context;

        public AddToFavouritesService(RealEstateContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNewFavourite(string userEmail, int advertID)
        {
            try
            {
                //E-posta adresine sahip olan kişi veritabanından bulunur. null olursa false dönülür ve ekleme işlemi yapılmaz.
                var person = await _context.Persons.FirstOrDefaultAsync(p => p.Email == userEmail);
                if (person == null)
                {
                    return false;
                }

                //Kişi ID değeri de kullanılarak favoriler tablosuna ekleme yapılır.
                var favouriteEntity = new Favourite
                {
                    AdvertID = advertID,
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
    }
}
