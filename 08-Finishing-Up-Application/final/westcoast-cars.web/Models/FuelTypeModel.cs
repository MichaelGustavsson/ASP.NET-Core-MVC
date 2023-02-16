namespace westcoast_cars.web.Models
{
    public class FuelTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<VehicleModel> Vehicles { get; set; }
    }
}