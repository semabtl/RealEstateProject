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
    public class ContactApplicationService: IContactApplicationService
    {
        private readonly RealEstateContext _context;

        public ContactApplicationService(RealEstateContext context)
        {
            _context = context;
        }

        public async Task<(bool success, string message)> AddContactApplication(ContactApplicationModel model)
        {
            using(var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var contactApplicationEntity = new ContactApplication
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        PhoneNumber = model.PhoneNumber,
                        MessageText = model.MessageText
                    };
                    _context.ContactApplications.Add(contactApplicationEntity);
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
