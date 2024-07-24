using Microsoft.EntityFrameworkCore;
using RealEstate.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DataAccess.Context
{
    public class RealEstateContext : DbContext
    {
        // Add-Migration RealEstate -o Migrations -Verbose
        // Update-Database 

        // Package Manager Console
        // Default Project : RealEstate.DataAccess

        public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyContactApplication> CompanyContactApplications { get; set; }
        public DbSet<ContactApplication> ContactApplications { get; set; }
        public DbSet<DecreasingPrice> DecreasingPrices { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<PaidAdvert> PaidAdverts { get; set; }
        public DbSet<PaidAdvertPrice> PaidAdvertPrices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Person> Persons { get; set; }



    }
}
