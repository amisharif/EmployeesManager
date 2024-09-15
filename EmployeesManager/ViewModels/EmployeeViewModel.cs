using System.ComponentModel.DataAnnotations;

namespace EmployeesManager.ViewModels
{
    public class EmployeeViewModel
    {
      
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
