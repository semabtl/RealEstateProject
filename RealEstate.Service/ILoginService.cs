using Microsoft.EntityFrameworkCore;
using RealEstate.DataAccess.Models;
using RealEstate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public interface ILoginService
    {
        bool Delete(int personId);

        bool CheckPerson(LoginModel model);
        Role FindUserRole(LoginModel model);
    }
}
