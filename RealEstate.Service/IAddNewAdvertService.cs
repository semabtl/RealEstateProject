﻿using RealEstate.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public interface IAddNewAdvertService
    {
        Task<(bool success, string message)> AddAdvertAsync(AddNewAdvertModel model, string userEmail);
    }
}
