using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entity
{
    public class City: BaseEntity
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string Region { get; set; }
    }
}
