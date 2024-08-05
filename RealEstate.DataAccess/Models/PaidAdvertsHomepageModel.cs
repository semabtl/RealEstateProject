using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DataAccess.Models
{
    public class PaidAdvertsHomepageModel
    {
        public int AdvertID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string? PathToImage { get; set; }

    }
}
