using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entity
{
    public class ContactApplication: BaseEntity
    {
        public int ContactApplicationID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string MessageText { get; set; }
    }
}
