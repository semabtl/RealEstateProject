using RealEstate.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DataAccess.Models
{
    public class ListAdvertsModel
    {
        public int AdvertID { get; set; }
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
        public string? PaidAdvertChoice { get; set; }
        public Status Status { get; set; }
        public bool IsFavourite { get; set; }
        public string? PathToImage { get; set; }

    }
}
