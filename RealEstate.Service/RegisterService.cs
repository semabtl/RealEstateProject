using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstate.DataAccess.Context;
using RealEstate.DataAccess.Models;
using RealEstate.Entity;

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
    }
    
}
