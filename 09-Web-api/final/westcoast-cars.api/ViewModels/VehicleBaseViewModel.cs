using System.ComponentModel.DataAnnotations;

namespace westcoast_cars.api.ViewModels
{
    public class VehicleBaseViewModel
    {
        [Required(ErrorMessage = "Tillverkare måste anges")]
        public string Make { get; set; }
        [Required(ErrorMessage = "Bilmodell måste anges")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Årsmodell måste anges")]
        public string ModelYear { get; set; }
        [Required(ErrorMessage = "Antal körda km måste anges")]
        public int Mileage { get; set; }
        [Required(ErrorMessage = "Bränsletyp måste anges")]
        public string FuelType { get; set; }
        [Required(ErrorMessage = "Typ av växellåda måste anges")]
        public string Transmission { get; set; }
        [Required(ErrorMessage = "Värde på bilen måste anges")]
        public int Value { get; set; }
        public string Description { get; set; }
        public bool IsSold { get; set; } = false;
        public string ImageUrl { get; set; }
    }
}