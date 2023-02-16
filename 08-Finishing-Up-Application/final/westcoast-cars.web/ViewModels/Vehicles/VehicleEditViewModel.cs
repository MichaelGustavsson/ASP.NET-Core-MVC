using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace westcoast_cars.web.ViewModels.Vehicles
{
    public class VehicleEditViewModel
    {
        public int Id { get; set; }
        [DisplayName("Tillverkare")]
        public int Manufacturer { get; set; }
        public List<SelectListItem> Manufacturers { get; set; }
        [DisplayName("Bränsle typ")]
        public int FuelType { get; set; }
        public List<SelectListItem> FuelTypes { get; set; }
        [DisplayName("Transmissions typ")]
        public int TransmissionsType { get; set; }
        public List<SelectListItem> TransmissionsTypes { get; set; }
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
        [DisplayName("Pris")]
        public int Value { get; set; }
        [DisplayName("Beskrivning")]
        public string Description { get; set; }
    }
}