using DuAn_QLCY.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList.Extensions;

namespace DuAn_QLCY.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QlCtPmContext _qtCtPmContext;


        public HomeController(ILogger<HomeController> logger, QlCtPmContext qtCtPmContext)
        {
            _logger = logger;
            _qtCtPmContext = qtCtPmContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DepartmentList(int? page)
        {
            var Department = _qtCtPmContext.Departments.ToPagedList(page ?? 1, 10);
            return Json(Department);
        }
        public class DepartmentRequest
        {
            public int DepartmentId { get; set; }
            // Add other properties as needed
        }

        [HttpPost]
        public IActionResult GetDepartmentById([FromBody] DepartmentRequest request)
        {
            if (request == null || request.DepartmentId <= 0)
            {
                return BadRequest(new { message = "Invalid department ID" });
            }

            var department = _qtCtPmContext.Departments
                .Include(d => d.Employees)
                .Where(d => d.DepartmentId == request.DepartmentId)
                .Select(d => new
                {
                    d.DepartmentId,
                    d.DepartmentName,
                    d.Description,
                    d.ActiveFrom,
                    d.ActiveTo,
                    Employees = d.Employees.Select(e => new
                    {
                        e.EmployeeId,
                        e.FirstName,
                        e.LastName,
                        e.Position,
                        e.JoiningDate
                    })
                })
                .FirstOrDefault();

            if (department == null)
            {
                return NotFound(new { message = "Department not found" });
            }

            return Json(department);
        }

        public IActionResult CreateDepartment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateDepartment(Department department)
        {
            _qtCtPmContext.Departments.Add(department);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("DepartmentList");
        }
        public IActionResult UpdateDepartment(int id)
        {
            var department = _qtCtPmContext.Departments.Where(t => t.DepartmentId == id).FirstOrDefault();
            return View(department);
        }
        [HttpPost]
        public IActionResult UpdateDepartment(Department department)
        {
            _qtCtPmContext.Departments.Update(department);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("DepartmentList");
        }
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            if (department == null)
            {
                return BadRequest("Department data is null.");
            }

            // Thêm phòng ban vào cơ sở dữ liệu
            _qtCtPmContext.Departments.Add(department);
            await _qtCtPmContext.SaveChangesAsync();

            return Ok(new { message = "Department added successfully" });
        }
        [HttpDelete]
        public IActionResult DeleteDepartment(int id)
        {
            var department = _qtCtPmContext.Departments.FirstOrDefault(t => t.DepartmentId == id);
            if (department == null)
            {
                return NotFound(new { message = "Department not found" });
            }

            _qtCtPmContext.Departments.Remove(department);
            _qtCtPmContext.SaveChanges();

            return Ok(new { message = "Department deleted successfully" });
        }
        public IActionResult EmployeeTypeList()
        {
            var employeetype = _qtCtPmContext.EmployeeTypes.ToList();
            return View(employeetype);
        }
        public IActionResult CreateEmployeeType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateEmployeeType(EmployeeType employeetype)
        {
            _qtCtPmContext.EmployeeTypes.Add(employeetype);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("EmployeeTypeList");
        }
        public IActionResult UpdateEmployeeType(int id)
        {
            var employeetype = _qtCtPmContext.EmployeeTypes.Where(t => t.TypeId == id).FirstOrDefault();
            return View(employeetype);
        }
        [HttpPost]
        public IActionResult UpdateEmployeeType(EmployeeType employeetype)
        {
            _qtCtPmContext.EmployeeTypes.Update(employeetype);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("EmployeeTypeList");
        }

        public IActionResult DeleteEmployeeType(int id)
        {
            var employeetype = _qtCtPmContext.EmployeeTypes.Where(t => t.TypeId == id).FirstOrDefault();
            _qtCtPmContext.Remove(employeetype);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("EmployeeTypeList");
        }




        public IActionResult EmployeesList(int? page)
        {
            var Employees = _qtCtPmContext.Employees.Include(a => a.Department).Include(a => a.Type).ToPagedList(page ?? 1, 10);
            return Json(Employees);
        }
        public IActionResult CreateEmployees()
        {
            var departments = _qtCtPmContext.Departments.ToList();
            var employeeTypes = _qtCtPmContext.EmployeeTypes.ToList();

            ViewBag.DepartmentsSelectList = new SelectList(departments, "DepartmentId", "DepartmentName");
            ViewBag.EmployeeTypesSelectList = new SelectList(employeeTypes, "TypeId", "TypeName");

            return View();
        }

        [HttpPost]
        public IActionResult CreateEmployees(Employee employees)
        {
            _qtCtPmContext.Employees.Add(employees);
            _qtCtPmContext.SaveChanges();

            return RedirectToAction("EmployeesList");
        }

        public IActionResult UpdateEmployees(int id)
        {
            var employees = _qtCtPmContext.Employees.Where(t => t.EmployeeId == id).FirstOrDefault();

            var departments = _qtCtPmContext.Departments.ToList();
            var employeeTypes = _qtCtPmContext.EmployeeTypes.ToList();
            ViewBag.DepartmentsSelectList = new SelectList(departments, "DepartmentId", "DepartmentName");
            ViewBag.EmployeeTypesSelectList = new SelectList(employeeTypes, "TypeId", "TypeName");

            return View(employees);
        }
        [HttpPost]
        public IActionResult UpdateEmployees(Employee employees)
        {
            _qtCtPmContext.Employees.Update(employees);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("EmployeesList");
        }
        public IActionResult DeleteEmployees(int id)
        {
            var employees = _qtCtPmContext.Employees.Where(t => t.EmployeeId == id).FirstOrDefault();
            _qtCtPmContext.Remove(employees);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("EmployeesList");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
