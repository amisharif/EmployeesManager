using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class EmployeeUpdateRequest
    {
        public Guid ID { get; set; }
        public string? Photo { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string? FullName { get; set; }


        public Employee ToEmployee()
        {
            return new Employee()
            {
                ID = ID,
                Photo = Photo,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Mobile = Mobile,
                DateOfBirth = DateOfBirth,
                FullName = FullName
            };
        }
    }

  

}
