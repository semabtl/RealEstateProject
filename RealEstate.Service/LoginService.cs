using RealEstate.DataAccess.Context;
using RealEstate.DataAccess.Models;
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

        public bool CheckPerson(LoginModel model)
        {
            Person? person = (from p in _context.Persons
                             where p.Email == model.Email && p.Password == model.Password && p.IsDeleted == false
                             select p).FirstOrDefault();

            if (person != null)
            {
                return true;
            }

            return false;
        }

        public Role FindUserRole(LoginModel model)
        {
            try
            {
                var user = _context.Persons.FirstOrDefault(p => p.Email == model.Email);

                return user.Role;
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
            
        }

    }
}


