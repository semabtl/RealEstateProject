using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entity
{
    public class Address: BaseEntity
    {
        public int AddressID { get; set; }
        public int CityID { get; set; }
        public int DistrictID { get; set; }
        public string StreetName { get; set; }
        public int BuildingNumber { get; set; }
        public int DoorNumber { get; set; }
        public string Country { get; set; }
    }
}
