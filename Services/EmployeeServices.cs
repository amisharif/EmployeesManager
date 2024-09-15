using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System.Web;

namespace Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly EmployeeDbContext _context;
        public EmployeeServices(EmployeeDbContext context)
        {
            _context = context;
        }

        public  EmployeeResponse AddEmployee(EmployeeAddRequest employeeAddRequest)
        {

            Employee emp = employeeAddRequest.ToEmployee();
            emp.ID = Guid.NewGuid();
            emp.FullName = employeeAddRequest.FirstName+" "+employeeAddRequest.LastName;

            _context.Add(emp);
            _context.SaveChanges();
            return emp.ToEmployeeResponse();
        }

        public bool DeleteEmployeeByID(string id)
        {
            Employee emp = _context.Employees.FirstOrDefault(emp=> emp.ID.ToString() == id);
            _context.Employees.Remove(emp);
            _context.SaveChanges();
            return true;
        }

        public List<EmployeeResponse> GetAllEmployees()
        {
            return _context.Employees.Select(temp => temp.ToEmployeeResponse()).ToList();
        }

        public EmployeeResponse GetEmployeeById(string id)
        {
            return _context.Employees.FirstOrDefault(temp=> temp.ID.ToString()==id).ToEmployeeResponse();
        }

        public List<EmployeeResponse> GetEmployeesByPageNo(int pageNo)
        {
            List<EmployeeResponse> employees = GetAllEmployees();

            return employees.Skip(((pageNo-1)*15)).Take(15).ToList();
        }

        public List<EmployeeResponse> GetFilteredPersons(List<EmployeeResponse>employees,string name, DateTime dateOfBirth, string email, string mobile)
        {
         //   string encodeEmail = HttpUtility.UrlEncode(email);
            List<EmployeeResponse> allPersons = employees;
            List<EmployeeResponse> matchingPersons = allPersons;

            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(email) && string.IsNullOrEmpty(mobile) )
                return matchingPersons;

            else if(!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(mobile))
            {
                matchingPersons = allPersons.Where(temp => temp.FullName.Contains(name) && temp.Mobile == mobile && temp.DateOfBirth == dateOfBirth && temp.Email.Contains(email)).ToList();
            }
            else
            {
               
                {
                    matchingPersons = allPersons.Where(temp => temp.FullName.Contains(name) && temp.Email.Contains(email)).ToList();
                }
               
            }

            return matchingPersons;

          
        }


        public List<EmployeeResponse> GetSortedPersons(List<EmployeeResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return allPersons;

            List<EmployeeResponse> sortedPersons = (sortBy, sortOrder) switch
            {
                (nameof(EmployeeResponse.FullName), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.FirstName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(EmployeeResponse.FullName), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.FirstName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(EmployeeResponse.Email), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(EmployeeResponse.Email), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(EmployeeResponse.DateOfBirth), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),

                (nameof(EmployeeResponse.DateOfBirth), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),

                (nameof(EmployeeResponse.Mobile), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),

                (nameof(EmployeeResponse.Mobile), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),

                _ => allPersons
            };

            return sortedPersons;
        }

        public EmployeeResponse UpdateEmployee(EmployeeUpdateRequest employeeUpdateRequest)
        {
            Employee employee = employeeUpdateRequest.ToEmployee();
            employee.FullName = employeeUpdateRequest.FirstName + " " + employeeUpdateRequest.LastName;
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
            return employee.ToEmployeeResponse();
        }
    }
}
