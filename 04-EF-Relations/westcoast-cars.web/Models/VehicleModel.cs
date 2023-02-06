using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace westcoast_cars.web.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Registeringsnummer saknas!")]
        [DisplayName("Registeringsnummer")]
        public string RegistrationNumber { get; set; }
        [Required(ErrorMessage = "Modell typ saknas!")]
        [DisplayName("Modell typ")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Årsmodell saknas")]
        [DisplayName("Årsmodell")]
        public string ModelYear { get; set; }
        [Required(ErrorMessage = "Antal km saknas!")]
        [Range(1, 500000, ErrorMessage = "Antal körda km saknas och får ej vara större än 50000 mil")]
        [DisplayName("Antal km")]
        public int Mileage { get; set; }

        // Navigation property...
        public int MakeId { get; set; }

        // Composition...
        [ForeignKey("MakeId")]
        public ManufacturerModel Manufacturer { get; set; }
    }
}