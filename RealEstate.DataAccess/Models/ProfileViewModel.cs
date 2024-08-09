using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DataAccess.Models
{
    public class ProfileViewModel
    {
        public UserInformationModel UserInformation { get; set; }
        public IEnumerable<ListAdvertsModel> AdvertsOfUser { get; set; }
    }
}
