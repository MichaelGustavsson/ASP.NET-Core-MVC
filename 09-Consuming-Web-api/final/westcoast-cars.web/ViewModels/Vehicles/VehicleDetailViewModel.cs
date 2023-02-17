namespace westcoast_cars.web.ViewModels.Vehicles
{
    public class VehicleDetailViewModel
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string ModelYear { get; set; }
        public int Mileage { get; set; }
        public string Fueltype { get; set; }
        public string Transmission { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public string ImageUrl { get; set; }
    }
}