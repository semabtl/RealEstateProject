using RealEstate.DataAccess.Context;
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
                    _context.SaveChanges();
                    return true;
                }
                
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

    }
}
