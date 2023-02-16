using System.ComponentModel.DataAnnotations;

namespace westcoast_cars.api.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}