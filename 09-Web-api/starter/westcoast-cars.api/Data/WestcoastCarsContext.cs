using Microsoft.EntityFrameworkCore;
using westcoast_cars.api.Entities;

namespace westcoast_cars.api.Data
{
    public class WestcoastCarsContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }
        public WestcoastCarsContext(DbContextOptions options) : base(options) { }
    }
}