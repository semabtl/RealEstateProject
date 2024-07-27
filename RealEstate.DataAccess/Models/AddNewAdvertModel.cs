using RealEstate.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DataAccess.Models
{
    public class AddNewAdvertModel
    {
        [Required(ErrorMessage = "Başlık alanı zorunludur.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Açıklama alanı zorunludur.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "İlan Türü alanı zorunludur.")]
        public ListingType ListingType { get; set; }

        [Required(ErrorMessage = "Emlak Türü zorunludur.")]
        public PropertyType PropertyType { get; set; }
        public float SquareMeters { get; set; }

        [Required(ErrorMessage = "Fiyat alanı zorunludur.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "İl adı alanı zorunludur.")]
        public string CityName { get; set; }
        public string Street { get; set; }
        public int BuildingNumber { get; set; }
        public int DoorNumber { get; set; }
        public string Country { get; set; }

        [Required(ErrorMessage = "İlçe adı alanı zorunludur.")]
        public string DistrictName { get; set; }
    }
}
