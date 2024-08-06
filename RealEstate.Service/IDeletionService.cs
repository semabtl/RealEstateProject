using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public interface IDeletionService
    {
        bool DeleteAnAdvert(int advertID);
        bool DeleteCompanyContactApplication(int companyContactApplicationID);
        bool DeleteContactApplicationMessage(int contactApplicationID);
        Task DeleteAdvertByPersonID(int personID);
        Task DeleteCompanyByPersonID(int personID);
        Task<bool> DeleteUser(int userID);
    }
}
