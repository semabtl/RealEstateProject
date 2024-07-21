using RealEstate.DataAccess.Context;
using RealEstate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public class LoginService : ILoginService
    {
        private readonly RealEstateContext _context;

        public LoginService(RealEstateContext context)
        {
            _context = context;
        }

        public bool Delete(int personId)
        {
            throw new NotImplementedException();
        }

        public Person Detail(string email, string password)
        {
            return (from p in _context.Persons
                    where p.Email == email && p.Password == password && p.IsDeleted == false
                    select p).FirstOrDefault();

        }

    }
}
