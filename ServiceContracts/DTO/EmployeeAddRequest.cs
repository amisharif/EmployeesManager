using Entities;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    public class EmployeeAddRequest 
    {


        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string? Photo { get; set; }
       

        public Employee ToEmployee()
        {
            return new Employee()
            {
                Photo = this.Photo,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Mobile = this.Mobile,
                DateOfBirth = this.DateOfBirth,
            };
        }


    }
}
