using RealEstate.DataAccess.Context;
using RealEstate.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public class ListUsersService : IListUsersService
    {
        private readonly RealEstateContext _context;

        public ListUsersService(RealEstateContext context)
        {
            _context = context;
        }

        public IEnumerable<UserInformationModel> ListAllUsers()
        {
            try
            {
                var users = from person in _context.Persons
                            join company in _context.Companies
                            on person.PersonID equals company.PersonID into personCompanies
                            from company in personCompanies.DefaultIfEmpty()
                            where person.IsDeleted == false
                            select new UserInformationModel
                            {
                                UserID = person.PersonID,
                                Email = person.Email,
                                Name = person.Name,
                                Surname = person.Surname,
                                PhoneNumber = person.PhoneNumber,
                                Role = person.Role,
                                CompanyName = company != null ? company.CompanyName : null
                            };

                return users.ToList();
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
            
        }

        public UserInformationModel GetUserInformationByEmail(string userEmail)
        {
            try
            {
                var user = (from person in _context.Persons
                            join company in _context.Companies
                            on person.PersonID equals company.PersonID into personCompanies
                            from company in personCompanies.DefaultIfEmpty()
                            where person.Email == userEmail
                            where person.IsDeleted == false
                            select new UserInformationModel
                            {
                                UserID = person.PersonID,
                                Email = person.Email,
                                Name = person.Name,
                                Surname = person.Surname,
                                PhoneNumber = person.PhoneNumber,
                                Role = person.Role,
                                RegistrationDate = person.CreateDate,
                                CompanyName = company != null ? company.CompanyName : null
                            }).SingleOrDefault();
                return user;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
