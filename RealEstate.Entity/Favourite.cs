using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entity
{
    public class Favourite: BaseEntity
    {
        public int FavouriteID { get; set; }
        public int AdvertID { get; set; }
        public int PersonID { get; set; }
    }
}
