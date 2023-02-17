using System.ComponentModel.DataAnnotations;

namespace westcoast_cars.api.ViewModels
{
    public class VehiclePostViewModel : VehicleBaseViewModel
    {
        [Required(ErrorMessage = "Regnummer måste anges")]
        public string RegistrationNumber { get; set; }
    }
}