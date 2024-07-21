using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Entity
{
    public class Image: BaseEntity
    {
        public int ImageID { get; set; }
        public int AdvertID { get; set; }
        public string PathToImage { get; set; }

    }
}
