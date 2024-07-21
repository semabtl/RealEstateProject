using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entity
{
    public class Person: BaseEntity
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public int CompanyID { get; set; }
       
    }

    public enum Role
    {
        Admin = 1,
        IndividualMember = 2,
        CorporateMember = 3
    }
}
