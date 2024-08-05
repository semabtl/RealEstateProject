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
        // ListAdvertsModel GetAdvertByID(int advertID);
        IEnumerable<ListAdvertsModel> ListAllAdverts();
    }
}
