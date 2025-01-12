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
        public IActionResult DepartmentList(int? page)
        {
            var Department = _qtCtPmContext.Departments.ToPagedList(page ?? 1, 10);
            return View(Department);
        }
        public IActionResult EmployeesList(int? page)
        {
            var Employees = _qtCtPmContext.Employees.Include(a => a.Department).Include(a => a.Type).ToPagedList(page ?? 1, 10);
            return View(Employees);
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
