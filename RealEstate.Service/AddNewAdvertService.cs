using Microsoft.EntityFrameworkCore;
using RealEstate.DataAccess.Context;
using RealEstate.DataAccess.Models;
using RealEstate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public class AddNewAdvertService : IAddNewAdvertService
    {
        
        private readonly RealEstateContext _context;

        public AddNewAdvertService(RealEstateContext context)
        {
            _context = context;
        }
        public async Task<(bool success, string message)> AddAdvertAsync(AddNewAdvertModel model, string userEmail)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var existingPerson = await _context.Persons.FirstOrDefaultAsync(p => p.Email == userEmail);

                    if (existingPerson == null)
                    {
                        return (false, "İlan kaydı yapılamadı");
                    }
                    var advertEntity = new Advert
                    {
                        PersonID = existingPerson.PersonID,
                        Title = model.Title,
                        Description = model.Description,
                        ListingType = model.ListingType,
                        PropertyType = model.PropertyType,
                        SquareMeters = model.SquareMeters,
                        Price = model.Price,
                        AddressID = null
                    };
                    _context.Adverts.Add(advertEntity);
                    await _context.SaveChangesAsync();

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
                    //İl ve ilçe ID değerleri de alınarak kurum adresi kaydedilir.
                    var addressEntity = new Address
                    {
                        CityID = city.CityID,
                        DistrictID = district.DistrictID,
                        StreetName = model.Street,
                        BuildingNumber = model.BuildingNumber,
                        DoorNumber = model.DoorNumber,
                        Country = model.Country,
                    };
                    _context.Addresses.Add(addressEntity);
                    await _context.SaveChangesAsync();

                    advertEntity.AddressID = addressEntity.AddressID;
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return (true, "Kayıt başarıyla tamamlandı.");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return (false, "İlan kaydı yapılamadı.");
                }
            }
            
        }

    }
}
