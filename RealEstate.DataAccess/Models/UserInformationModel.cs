using RealEstate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DataAccess.Models
{
    public class UserInformationModel
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public Role Role { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? CompanyName { get; set; }
    }
}
