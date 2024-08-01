using RealEstate.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public interface IContactApplicationService
    {
        Task<(bool success, string message)> AddContactApplication(ContactApplicationModel model);
        Task<(bool success, string message)> AddCompanyContactApplication(CompanyContactApplicationModel model);
    }
}
