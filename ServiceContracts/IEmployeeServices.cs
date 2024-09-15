using Entities;
using Microsoft.Data.SqlClient;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceContracts
{
    public interface IEmployeeServices
    {
        public EmployeeResponse AddEmployee(EmployeeAddRequest employeeAddRequest);
        public List<EmployeeResponse> GetAllEmployees();
        public EmployeeResponse GetEmployeeById(string id);

        List<EmployeeResponse> GetFilteredPersons(List<EmployeeResponse>employees,string name,DateTime dateOfBirth,string email,string mobile);

        public List<EmployeeResponse> GetSortedPersons(List<EmployeeResponse>employees,string sortBy,SortOrderOptions sortOrder);

        public List<EmployeeResponse> GetEmployeesByPageNo(int pageNo);

        public EmployeeResponse UpdateEmployee(EmployeeUpdateRequest employeeUpdateRequest);
        public bool DeleteEmployeeByID(string id);

    }
}
