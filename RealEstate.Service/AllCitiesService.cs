using RealEstate.DataAccess.Context;
using RealEstate.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public class AllCitiesService : IAllCitiesService
    {
        private readonly RealEstateContext _context;

        public AllCitiesService(RealEstateContext context)
        {
            _context = context;
        }

        public IEnumerable<AllCitiesModel> GetAllCities()
        {
            var allCityNames = _context.Cities
            .Select(c => new AllCitiesModel { CityName = c.CityName })
            .ToList();

            return allCityNames;
        }
    }
}
