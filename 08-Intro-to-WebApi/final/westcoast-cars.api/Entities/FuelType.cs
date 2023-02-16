namespace westcoast_cars.api.Entities
{
    public class FuelType : BaseEntity
    {
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}