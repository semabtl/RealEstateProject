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
        public async Task<(bool success, string message, int advertID)> AddAdvertAsync(AddNewAdvertModel model, string userEmail)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var existingPerson = await _context.Persons.FirstOrDefaultAsync(p => p.Email == userEmail);

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
                        return (false, "İl adı yanlış.", 0);
                    }

                    //Girilen ada sahip ilçenin varlığı kontrol edilir.
                    var district = await _context.Districts.FirstOrDefaultAsync(d => d.DistrictName == model.DistrictName);
                    if (district == null)
                    {
                        return (false, "İlçe adı yanlış.", 0);
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
                    return (true, "Kayıt başarıyla tamamlandı.", advertEntity.AdvertID);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return (false, "İlan kaydı yapılamadı.", 0);
                }
            }
            
        }

        public async Task<(bool success, string message)> AddPaidAdvertAsync(string userEmail, int advertID)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync()) {
                try
                {
                    var paidAdvertPricesObject = await _context.PaidAdvertPrices.FirstOrDefaultAsync(p => p.Title == model.PaidAdvertChoice);
                    if (paidAdvertPricesObject == null)
                    {
                        return (false, "Ücretli ilan türü bulunamadı.");
                    }

                    var paidAdvertEntity = new PaidAdvert
                    {
                        PaidTypeID = paidAdvertPricesObject.ID,
                        AdvertID = advertID
                    };
                    _context.PaidAdverts.Add(paidAdvertEntity);
                    await _context.SaveChangesAsync();

                    var person = await _context.Persons.FirstOrDefaultAsync(p => p.Email == userEmail);
                    if (person == null)
                    {
                        return (false, "Kişi bulunamadı.");
                    }

                    var paymentEntity = new Payment
                    {
                        PersonID = person.PersonID,
                        PaidAdvertID = paidAdvertEntity.PaidAdvertID,
                        Amount = paidAdvertPricesObject.Price
                    };
                    _context.Payments.Add(paymentEntity);
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
//iki sssyrı metod olsun diğerinde saydece payment olsun, adres olmasın.
//ücretsizse aktif 1 yap. ücretliyse 0 olsun. ödeme yapınca 1'e çevirirsin.
