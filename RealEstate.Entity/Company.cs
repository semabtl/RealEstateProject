using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entity
{
    public class Company: BaseEntity
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public int AddressID { get; set; }
        public int PersonID { get; set; }

    }
}
