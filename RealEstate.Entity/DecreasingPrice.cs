using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entity
{
    public class DecreasingPrice: BaseEntity
    {
        public int AdvertID { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
    }
}
