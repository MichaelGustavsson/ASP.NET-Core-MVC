using Microsoft.EntityFrameworkCore;
using westcoast_cars.web.Models;

namespace westcoast_cars.web.Data
{
    public class WestcoastCarsContext : DbContext
    {
        public DbSet<VehicleModel> Vehicles { get; set; }
        public DbSet<ManufacturerModel> Manufacturers { get; set; }
        public DbSet<FuelTypeModel> FuelTypes { get; set; }
        public DbSet<TransmissionsTypeModel> TransmissionsTypes { get; set; }
        public WestcoastCarsContext(DbContextOptions options) : base(options) { }

    }
}