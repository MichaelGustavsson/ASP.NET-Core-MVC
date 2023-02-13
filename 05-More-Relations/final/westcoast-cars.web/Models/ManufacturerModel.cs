namespace westcoast_cars.web.Models
{
    public class ManufacturerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property...
        public ICollection<VehicleModel> Vehicles { get; set; }
    }
}