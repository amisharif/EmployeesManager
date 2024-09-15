using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class EmployeeResponse
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

        public EmployeeUpdateRequest ToEmployeeUpdateRequest()
        {
            return new EmployeeUpdateRequest()
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

    public static class EmployeeExtension
    {
       public static EmployeeResponse ToEmployeeResponse(this Employee emp)
        {
            return new EmployeeResponse {ID=emp.ID, Photo = emp.Photo, FirstName = emp.FirstName, LastName = emp.LastName, Email = emp.Email,Mobile=emp.Mobile,DateOfBirth=emp.DateOfBirth,FullName=emp.FirstName+" "+emp.LastName };
        }
    }
}
