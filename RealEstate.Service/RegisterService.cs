using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstate.DataAccess.Context;
using RealEstate.DataAccess.Models;
using RealEstate.Entity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RealEstate.Service
{
    public class RegisterService : IRegisterService
    {
        private readonly RealEstateContext _context;

        public RegisterService(RealEstateContext context)
        {
            _context = context;
        }

   
        public async Task<(bool success, string message)> AddPersonAsync(PersonalRegisterModel model)
        {
            try
            {
                var existingPerson = await _context.Persons
            .FirstOrDefaultAsync(p => p.Email == model.Email);

                if (existingPerson != null)
                {
                    return (false, "Bu e-posta adresi kullanılmaktadır.");
                }

                var entity = new Person
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Password = model.Password,
                    Role = Role.IndividualMember
                };

                _context.Persons.Add(entity);
                await _context.SaveChangesAsync();
                return (true, "Kayıt İşlemi Tamamlandı");
            }
            catch (Exception)
            {
                return (false, "Hata: Kayıt işlemi yapılamadı.");
            }
        }

        public async Task<(bool success, string message)> AddCorporateAccountAsync(CorporateRegisterModel model) {

            using(var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var existingPerson = await _context.Persons.FirstOrDefaultAsync(p => p.Email == model.Email);

                    if (existingPerson != null)
                    {
                        return (false, "Bu e-posta adresi kullanılmaktadır.");
                    }
                    
                    
                    //Kişi, kişiler tablosuna eklenir.
                    var personEntity = new Person
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        Password = model.Password,
                        Role = Role.CorporateMember,
                        CompanyID = null
                    };
                    _context.Persons.Add(personEntity);
                    await _context.SaveChangesAsync();

                    //Kurum, kurumlar tablosuna eklenir.
                    var companyEntity = new Company
                    {
                        CompanyName = model.CompanyName,
                        AddressID = null,
                        PersonID = personEntity.PersonID,
                    };
                    _context.Companies.Add(companyEntity);
                    await _context.SaveChangesAsync();

                    //Kurum eklendikten sonra, kişi tablosunda null olan kurum id değeri güncellenir.
                    personEntity.CompanyID = companyEntity.CompanyID;
                    _context.Persons.Update(personEntity);
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

                    //Kurum kaydındaki adres ID değeri güncellenir.
                    companyEntity.AddressID = addressEntity.AddressID;
                    _context.Companies.Update(companyEntity);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return (true, "Kayıt başarıyla tamamlandı.");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return (false, "Kayıt işlemi yapılamadı.");
                }
            }
        }
    }
    
}
