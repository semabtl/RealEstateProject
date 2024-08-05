using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entity
{
    public class PaidAdvert : BaseEntity
    {
        public int PaidAdvertID { get; set; }
        public int AdvertID { get; set; }
        public int PaidTypeID { get; set; }
    }
}
