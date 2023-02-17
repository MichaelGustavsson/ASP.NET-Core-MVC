namespace westcoast_cars.api.Entities
{
    public class Manufacturer : BaseEntity
    {
        // Navigation property...
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}