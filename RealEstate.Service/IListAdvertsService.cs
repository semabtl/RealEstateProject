using RealEstate.DataAccess.Models;
using RealEstate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public interface IListAdvertsService
    {
        IEnumerable<ListAdvertsModel> FindAdvertsByCity(string userEmail, string cityName);       
        IEnumerable<ListAdvertsModel> ListAllAdverts();
        IEnumerable<ListAdvertsModel> FindAdvertsOfUser(string userEmail);
        IEnumerable<ListAdvertsModel> FindFilteredAdverts(string userEmail, RealEstate.Entity.PropertyType propertyType, RealEstate.Entity.ListingType listingType);
    }
}
