using Microsoft.EntityFrameworkCore;

namespace Parkingbackend.Models
{
    public class ParkingContext : DbContext
    {
        public DbSet<Parking> Parkinzi {get; set;}
        public DbSet<Polje> Polja {get; set;}
        public DbSet<Automobil> Automobili {get; set;}

        public ParkingContext(DbContextOptions options) : base(options)
        {
            
        }

    }
}