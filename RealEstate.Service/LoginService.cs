using RealEstate.DataAccess.Context;
using RealEstate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

        public bool CheckPerson(string email, string password)
        {
            Person person = (from p in _context.Persons
                             where p.Email == email && p.Password == password && p.IsDeleted == false
                             select p).FirstOrDefault();

            if (person != null)
            {
                return true;
            }

            return false;
        }

    }
}


