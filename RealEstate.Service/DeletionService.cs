using Microsoft.EntityFrameworkCore;
using RealEstate.DataAccess.Context;
using RealEstate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public class DeletionService : IDeletionService
    {
        private readonly RealEstateContext _context;

        public DeletionService(RealEstateContext context)
        {
            _context = context;
        }

        public bool DeleteAnAdvert(int advertID)
        {
            try
            {
                var advert = _context.Adverts.FirstOrDefault(a => a.AdvertID == advertID);
                if (advert != null)
                {
                    advert.IsDeleted = true;
                    advert.UpdateDate = DateTime.Now;

                    //İlan ücretli ilanlar tablosunda yer alıyorsa, o kayıt da silinmelidir.
                    var paidAdvert = _context.PaidAdverts.FirstOrDefault(p => p.AdvertID == advertID);
                    if(paidAdvert != null)
                    {
                         paidAdvert.IsDeleted = true;
                         paidAdvert.UpdateDate = DateTime.Now;
                        
                    }

                    //İlana ait görseller varsa silinir.
                    var images = _context.Images.Where(i => i.AdvertID == advertID).ToList();
                    if (images.Count > 0)
                    {
                        foreach (var image in images)
                        {
                            image.IsDeleted = true;
                            image.UpdateDate = DateTime.Now;
                        }
                    }

                    //İlana ait adres kaydı silinir.
                    var address = _context.Addresses.FirstOrDefault(a => a.AddressID == advert.AddressID);
                    if (address != null)
                    {
                        address.IsDeleted = true;
                        address.UpdateDate = DateTime.Now;
                    }

                    _context.SaveChanges();
                    return true;
                }
                
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAdvertByPersonID(int personID)
        {
            try
            {
                //Kişiye ait ilanlar bulunur.
                var advertsOfPerson = await _context.Adverts.Where(a => a.PersonID == personID).ToListAsync();
                if(advertsOfPerson.Count > 0)
                {
                    //Her ilan için ilgili tablolardaki kayıtların da güncellenmesi gerekmektedir.
                    foreach(var advert in advertsOfPerson)
                    {
                        advert.IsDeleted = true;
                        advert.UpdateDate = DateTime.Now;

                        //İlan ücretli ilanlar tablosunda yer alıyorsa, o kayıt da silinmelidir.
                        var paidAdvert = await _context.PaidAdverts.FirstOrDefaultAsync(p => p.AdvertID == advert.AdvertID);
                        if (paidAdvert != null)
                        {
                            paidAdvert.IsDeleted = true;
                            paidAdvert.UpdateDate = DateTime.Now;
                        }

                        //İlana ait görseller varsa silinir.
                        var images = await _context.Images.Where(i => i.AdvertID == advert.AdvertID).ToListAsync();
                        if (images.Count > 0)
                        {
                            foreach (var image in images)
                            {
                                image.IsDeleted = true;
                                image.UpdateDate = DateTime.Now;
                            }
                        }

                        //İlana ait adres kaydı silinir.
                        var address = await _context.Addresses.FirstOrDefaultAsync(a => a.AddressID == advert.AddressID);
                        if (address != null)
                        {
                            address.IsDeleted = true;
                            address.UpdateDate = DateTime.Now;
                        }

                    }
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteCompanyContactApplication(int companyContactApplicationID) 
        {
            try
            {
                var application = _context.CompanyContactApplications.FirstOrDefault(a => a.CompanyContactApplicationID == companyContactApplicationID);
                if (application != null)
                {
                    application.IsDeleted = true;
                    application.UpdateDate = DateTime.Now;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch( Exception ex ) 
            { 
                throw new Exception(ex.Message);
            }
           
        }
        public bool DeleteContactApplicationMessage(int contactApplicationID)
        {
            try
            {
                var message = _context.ContactApplications.FirstOrDefault(c => c.ContactApplicationID == contactApplicationID);
                if(message != null)
                {
                    message.IsDeleted = true;
                    message.UpdateDate = DateTime.Now;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteCompanyByPersonID(int personID)
        {
            try
            {
                var company = await _context.Companies.FirstOrDefaultAsync(c => c.PersonID == personID);
                if(company != null)
                {
                    company.IsDeleted = true;
                    company.UpdateDate = DateTime.Now;

                    var address = await _context.Addresses.FirstOrDefaultAsync(a => a.AddressID == company.AddressID);
                    if(address != null)
                    {
                        address.IsDeleted = true;
                        address.UpdateDate = DateTime.Now;
                    }
                    await _context.SaveChangesAsync();
                }
            }
            catch( Exception ex )
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteUser(int userID)
        {
            try
            {
                var user = await _context.Persons.FirstOrDefaultAsync(p => p.PersonID == userID);
                if(user != null)
                {
                    user.IsDeleted = true;
                    user.UpdateDate = DateTime.Now;

                    
                    //Kullanıcının verdiğği ilan ve onunla bağlantılı tablolardaki kayıtların güncellenmesi için metot çağrılır.
                    await DeleteAdvertByPersonID(userID);

                    //Kişinin hesabı kurumsal hesap ise, kurum bilgileri de güncellenmelidir.
                    if(user.CompanyID != null)
                    {
                       await DeleteCompanyByPersonID(userID);
                    }

                    var favourites = await _context.Favourites.Where(f => f.PersonID == userID).ToListAsync();
                    foreach(var favourite in favourites)
                    {
                        favourite.IsDeleted = true;
                        favourite.UpdateDate = DateTime.Now;
                    }

                    await _context.SaveChangesAsync();
                    return true;
                    
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteNews(int newsID)
        {
            try
            {
                var news = await _context.News.Where(n => n.NewsID == newsID).SingleOrDefaultAsync();
                if(news != null)
                {
                    news.IsDeleted = true;
                    news.UpdateDate = DateTime.Now;

                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
