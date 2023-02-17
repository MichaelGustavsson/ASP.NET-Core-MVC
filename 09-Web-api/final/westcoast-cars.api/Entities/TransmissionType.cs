namespace westcoast_cars.api.Entities
{
    public class TransmissionType : BaseEntity
    {
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}