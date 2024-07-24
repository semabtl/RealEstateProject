using System.ComponentModel.DataAnnotations;

namespace RealEstate.Web.Models
{
    public class CorporateRegisterModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public string CityName { get; set; }
        public string Street { get; set; }
        public int BuildingNumber { get; set; }
        public int DoorNumber { get; set; }
        public string Country { get; set; } 
        public string DistrictName { get; set; }
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "E-Posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
