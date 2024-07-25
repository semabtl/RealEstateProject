using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.DataAccess.Models;
using RealEstate.Entity;

namespace RealEstate.Service
{
    public interface IRegisterService
    {
        Task<(bool success, string message)> AddPersonAsync(PersonalRegisterModel model);
        Task<(bool success, string message)> AddCorporateAccountAsync(CorporateRegisterModel model);

    }
}
