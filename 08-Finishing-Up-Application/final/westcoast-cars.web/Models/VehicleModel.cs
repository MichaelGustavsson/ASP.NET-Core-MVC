using System.ComponentModel.DataAnnotations.Schema;

namespace westcoast_cars.web.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string Model { get; set; }
        public string ModelYear { get; set; }
        public int Mileage { get; set; }
        public string ImageUrl { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
        public bool IsSold { get; set; }
        // Navigation property...
        public int MakeId { get; set; }
        // Composition...
        [ForeignKey("MakeId")]
        public ManufacturerModel Manufacturer { get; set; }

        public int FuelTypeId { get; set; }
        [ForeignKey("FuelTypeId")]
        public FuelTypeModel FuelType { get; set; }

        public int TransmissionsTypeId { get; set; }
        [ForeignKey("TransmissionsTypeId")]
        public TransmissionsTypeModel TransmissionsType { get; set; }
    }
}