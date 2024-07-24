using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entity
{
    public class CompanyContactApplication: BaseEntity
    {
        public int CompanyContactApplicationID { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public int CityID { get; set; }
        public int DistrictID { get; set; }
    }
}
