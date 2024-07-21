using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entity
{
    public class District: BaseEntity
    {
        public int DistrictID { get; set; }
        public int CityID { get; set; }
        public string DistrictName { get; set; }
    }
}
