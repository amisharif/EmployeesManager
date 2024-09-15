using EmployeesManager.Models;
using EmployeesManager.ViewModels;
using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Web;

namespace EmployeesManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeServices _employeeServices;
        public HomeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }

        public IActionResult Index(string name, DateTime dateOfBirth, string email, string mobile, string sortBy = nameof(Employee.FirstName), SortOrderOptions sortOrder = SortOrderOptions.ASC, int page = 1)
        {


            List<EmployeeResponse> employees = _employeeServices.GetEmployeesByPageNo(page);

            List<EmployeeResponse> persons = _employeeServices.GetFilteredPersons(employees, name, dateOfBirth, email, mobile);

            //Sort
            List<EmployeeResponse> sortedPersons = _employeeServices.GetSortedPersons(persons, sortBy, sortOrder);

            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();

            ViewBag.CurrentName = name;
            ViewBag.CurrentDateOfBirth = dateOfBirth;
            ViewBag.CurrentEmail = email;
            ViewBag.CurrentMobile = mobile;
            ViewBag.CurrentPage = page;

            int totalEmployees = _employeeServices.GetAllEmployees().ToArray().Count();
            int totalPage = (totalEmployees + 15 - 1) / 15;
            ViewBag.TotalPage = totalPage;
            ViewBag.TotalEmployees = totalEmployees;
            ViewBag.From = ((page - 1) * 15 + 1);
            if (page * 15 > totalEmployees) ViewBag.To = totalEmployees;
            else
            {
                ViewBag.To = page * 15;
            }


            return View(sortedPersons);
        }

        [HttpGet]
        public IActionResult EmployeeTable(string name)
        {
            return View();
        }



        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }


        [HttpPost]
        public IActionResult Create(EmployeeAddRequest employeeAddRequest, IFormFile file)
        {

            string fileName = string.Empty;
            string path = string.Empty;

            if (file.Length > 0)
            {
                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images"));

                string fullPath = Path.Combine(path, fileName);
                using (var image = Image.Load(file.OpenReadStream()))
                {
                    string newSize = ResizeImage(image, 300, 300);
                    string[] aSize = newSize.Split(',');
                    image.Mutate(h => h.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));
                    image.Save(fullPath);
                }

                employeeAddRequest.Photo = fileName;
            }

            EmployeeResponse response = _employeeServices.AddEmployee(employeeAddRequest);
            return RedirectToAction("Index", "Home");

        }


        [HttpGet]
        public IActionResult Edit(string id)
        {
            EmployeeResponse employeeResponse = _employeeServices.GetEmployeeById(id);
            if(employeeResponse==null)RedirectToAction ("Index", "Home");

            EmployeeUpdateRequest employeeUpdateRequest = employeeResponse.ToEmployeeUpdateRequest();
            return PartialView(employeeUpdateRequest);

        }


        [HttpPost]
        public IActionResult Edit(string id,EmployeeUpdateRequest employeeUpdateRequest)
        {

            EmployeeResponse employeeResponse = _employeeServices.UpdateEmployee(employeeUpdateRequest);
            if (employeeResponse == null) return PartialView("Update");
            return RedirectToAction("Index");

        }


        public IActionResult Delete(string id)
        {
            bool isSuccess = _employeeServices.DeleteEmployeeByID(id);
            if (isSuccess)
            {
                return RedirectToAction("Index", "Home");
            }else
            {
                return BadRequest();
            }
           
        }


      




        public string ResizeImage(Image img,int maxWidth,int  maxHeight)
        {
            if (img.Width > maxWidth || img.Height > maxHeight)
            {
                double widthRatio = (double)img.Width / (double)maxWidth;
                double heightRatio = (double)img.Height / (double)maxHeight;
                double ratio = Math.Max(widthRatio, heightRatio);
                int newWidth = (int)(img.Width / ratio);
                int newHeight = (int)(img.Height / ratio);
                return newHeight.ToString() + "," + newWidth.ToString();
            }
            else
            {
                return img.Height.ToString() + "," + img.Width.ToString();
            }

        }




    }
}
