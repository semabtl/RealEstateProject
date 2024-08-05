using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DataAccess.Models
{
    public class FavouritesModel
    {
        public string UserEmail { get; set; }
        public int AdvertID { get; set; } 
        public string CityName { get; set; } 
        public string PathToImage { get; set; }
    }
}
