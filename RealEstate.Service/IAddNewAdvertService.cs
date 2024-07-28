using RealEstate.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public interface IAddNewAdvertService
    {
        Task<(bool success, string message, int advertID)> AddAdvertAsync(AddNewAdvertModel model, string userEmail);
        Task<(bool success, string message)> AddPaidAdvertAsync(string userEmail, int advertID);
    }
}
