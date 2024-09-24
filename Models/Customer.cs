using System.ComponentModel.DataAnnotations;

namespace ApiPro.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Age is required")]

        public int Age {  get; set; }   
    }
}
