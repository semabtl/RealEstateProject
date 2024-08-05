using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entity
{
    public class Payment : BaseEntity
    {
        public int PaymentID { get; set; }
        public int PersonID { get; set; }
        public int PaidAdvertID { get; set; } 
        public float Amount { get; set; }

    }
}
