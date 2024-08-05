using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entity
{
    public class News : BaseEntity
    {
        public int NewsID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorID { get; set; }

    }
}
