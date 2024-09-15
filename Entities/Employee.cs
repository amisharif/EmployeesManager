using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Employee
    {
        [Key]
        public Guid ID { get; set; }
        public string? Photo { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required(ErrorMessage ="Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage ="Phone is required")]
        public string? Mobile  { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string? FullName { get; set; }
    }
}
