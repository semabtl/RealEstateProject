using RealEstate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DataAccess.Models
{
    public class AdvertDetailsModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ListingType ListingType { get; set; }
        public PropertyType PropertyType { get; set; }
        public float SquareMeters { get; set; }
        public double Price { get; set; }
        public string CityName { get; set; }
        public string Street { get; set; }
        public int BuildingNumber { get; set; }
        public int DoorNumber { get; set; }
        public string Country { get; set; }
        public string DistrictName { get; set; }
        public string AdvertiserName { get; set; }
        public string AdvertiserSurname { get; set; }
        public string AdvertiserPhoneNumber { get; set; }
        public string? AdvertiserCompanyName { get; set; }
        public List<string>? ImagePaths { get; set; }

    }
}
