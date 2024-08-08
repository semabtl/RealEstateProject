using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DataAccess.Models
{
    public class NewsModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public DateTime PublishedDate { get; set; }
        public int NewsID { get; set; }
        //public string? PathToImage { get; set; }
    }
}
