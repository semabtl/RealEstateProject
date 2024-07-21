using Microsoft.EntityFrameworkCore;
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
        Person Detail(string email, string password);

        bool Delete(int personId);
    }
}
