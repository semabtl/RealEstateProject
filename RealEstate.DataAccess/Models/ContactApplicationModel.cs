using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DataAccess.Models
{
    public class ContactApplicationModel
    {
        public int ContactApplicationID { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Telefon numarası alanı zorunludur.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Mesaj alanı zorunludur.")]
        public string MessageText { get; set; }

    }
}
