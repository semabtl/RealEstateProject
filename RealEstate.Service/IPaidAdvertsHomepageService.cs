﻿using RealEstate.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Service
{
    public interface IPaidAdvertsHomepageService
    {
        IEnumerable<PaidAdvertsHomepageModel> GetFeaturedAdverts();
    }
}
