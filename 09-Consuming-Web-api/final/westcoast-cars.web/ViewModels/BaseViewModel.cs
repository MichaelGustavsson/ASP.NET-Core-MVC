using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace westcoast_cars.web.ViewModels
{
    public class BaseViewModel
    {
        [Required(ErrorMessage = "Namn måste anges")]
        [DisplayName("Namn")]
        public string Name { get; set; }
    }
}