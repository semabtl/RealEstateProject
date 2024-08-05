using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DataAccess.Models
{
    public class CompanyContactApplicationModel
    {
        [Required(ErrorMessage = "Şirket adı alanı zorunludur.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Yetkili adı soyadı alanı zorunludur.")]
        public string NameOfCompanyOfficial { get; set; }

        [Required(ErrorMessage = "Yetkili telefon numarası alanı zorunludur.")]
        public string PhoneNumber { get; set;}

        [Required(ErrorMessage = "Şirket ili alanı zorunludur.")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Şirket ilçesi alanı zorunludur.")]
        public string DistrictName { get; set; }
        public int CompanyContactApplicationID { get; set; }
    }
}
