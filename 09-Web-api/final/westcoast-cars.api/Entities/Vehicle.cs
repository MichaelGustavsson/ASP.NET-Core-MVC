using System.ComponentModel.DataAnnotations.Schema;

namespace westcoast_cars.api.Entities
{
    public class Vehicle : BaseEntity
    {
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
        public Manufacturer Manufacturer { get; set; }

        public int FuelTypeId { get; set; }
        [ForeignKey("FuelTypeId")]
        public FuelType FuelType { get; set; }

        public int TransmissionsTypeId { get; set; }
        [ForeignKey("TransmissionsTypeId")]
        public TransmissionType TransmissionsType { get; set; }
    }
}