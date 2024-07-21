using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entity
{
    public  class Advert: BaseEntity
    {
        public int AdvertID { get; set; }
        public int AddressID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public ListingType ListingType { get; set; }
        public PropertyType PropertyType { get; set; }
        public float SquareMeters { get; set; }
        public double Price { get; set; }
        public int PersonID { get; set; }

    }

    public enum ListingType
    {
        ForSale = 1,
        ForRent = 2
    }

    public enum PropertyType
    {
        House = 1,
        Land = 2,
        Workplace = 3
    }
}
