using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public interface IAddToFavouritesService
    {
        Task<bool> AddNewFavourite(string userEmail, int advertID);
    }
}
