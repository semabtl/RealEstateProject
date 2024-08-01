using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DataAccess.Models
{
    public class HomepageViewModel
    {
        public IEnumerable<AllCitiesModel> AllCities { get; set; }
        public IEnumerable<PaidAdvertsHomepageModel> PaidHomepageAdverts { get; set; }
    }
}
