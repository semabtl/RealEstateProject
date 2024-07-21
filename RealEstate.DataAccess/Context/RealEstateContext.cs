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

        public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //Entity'ler buraya eklenmelidir. (DB Tablolarin)
        public DbSet<Person> Persons { get; set; }
    }
}
