using System.ComponentModel.DataAnnotations;

namespace westcoast_cars.api.ViewModels
{
    public class PostViewModel
    {
        [Required(ErrorMessage = "Namn måste anges")]
        public string Name { get; set; }
    }
}