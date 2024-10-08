﻿using RealEstate.DataAccess.Context;
using RealEstate.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public class PaidAdvertsHomepageService : IPaidAdvertsHomepageService
    {
        private readonly RealEstateContext _context;

        public PaidAdvertsHomepageService(RealEstateContext context)
        {
            _context = context;
        }

        public IEnumerable<PaidAdvertsHomepageModel> GetFeaturedAdverts()
        {
            
            var result = from advert in _context.Adverts
                         join paidAdvert in _context.PaidAdverts on advert.AdvertID equals paidAdvert.AdvertID
                         join address in _context.Addresses on advert.AddressID equals address.AddressID
                         join city in _context.Cities on address.CityID equals city.CityID
                         join district in _context.Districts on address.DistrictID equals district.DistrictID
                         join paidAdvertPrice in _context.PaidAdvertPrices on paidAdvert.PaidTypeID equals paidAdvertPrice.ID
                         where paidAdvertPrice.Title == "Anasayfada Öne Çıkar"
                         where advert.IsDeleted == false
                         select new PaidAdvertsHomepageModel
                         {
                             AdvertID = advert.AdvertID,
                             Title = advert.Title,
                             Description = advert.Description,
                             Price = advert.Price,
                             CityName = city.CityName,
                             DistrictName = district.DistrictName
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
    }
}
