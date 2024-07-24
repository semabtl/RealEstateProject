using System.ComponentModel.DataAnnotations;

namespace RealEstate.DataAccess.Models

public class PersonalRegisterModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "E-Posta alanı zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Şifre alanı zorunludur.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
