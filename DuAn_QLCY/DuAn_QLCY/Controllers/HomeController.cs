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

        public IActionResult DeleteDepartment(int id)
        {
            var department = _qtCtPmContext.Departments.Where(t => t.DepartmentId == id).FirstOrDefault();
            _qtCtPmContext.Remove(department);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("DepartmentList");
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



        public IActionResult ProjectList(int? page)
        {
            var Project = _qtCtPmContext.Projects.Include(a => a.Manager).Include(a => a.Client).ToPagedList(page ?? 1, 10);
            return View(Project);
        }
        public IActionResult CreateProject()
        {
            var managers = _qtCtPmContext.Employees.ToList();
            var clients = _qtCtPmContext.Clients.ToList();

            ViewBag.managersSelectList = new SelectList(managers, "EmployeeId", "LastName");
            ViewBag.clientsSelectList = new SelectList(clients, "ClientId", "ContactName");

            return View();
        }

        [HttpPost]
        public IActionResult CreateProject(Project project)
        {
            _qtCtPmContext.Projects.Add(project);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("ProjectList");
        }

        public IActionResult UpdateProject(int id)
        {
            var project = _qtCtPmContext.Projects.Where(t => t.ProjectId == id).FirstOrDefault();

            var managers = _qtCtPmContext.Employees.ToList();
            var clients = _qtCtPmContext.Clients.ToList();
            ViewBag.managersSelectList = new SelectList(managers, "EmployeeId", "LastName");
            ViewBag.clientsSelectList = new SelectList(clients, "ClientId", "ContactName");

            return View(project);
        }
        [HttpPost]
        public IActionResult UpdateProject(Project project)
        {
            _qtCtPmContext.Projects.Update(project);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("ProjectList");
        }
        public IActionResult DeleteProject(int id)
        {
            var project = _qtCtPmContext.Projects.Where(t => t.ProjectId == id).FirstOrDefault();
            _qtCtPmContext.Remove(project);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("ProjectList");
        }



        public IActionResult EmploymentContractsList(int? page)
        {
            var EmploymentContracts = _qtCtPmContext.EmploymentContracts.Include(a => a.Employee).ToPagedList(page ?? 1, 10);
            return View(EmploymentContracts);
        }
        public IActionResult CreateEmploymentContracts()
        {
            var Employee = _qtCtPmContext.EmploymentContracts.ToList();
            ViewBag.EmployeesSelectList = new SelectList(Employee, "EmployeeId", "LastName");
            return View();
        }

        [HttpPost]
        public IActionResult CreateEmploymentContracts(EmploymentContract EmploymentContracts)
        {
            _qtCtPmContext.EmploymentContracts.Add(EmploymentContracts);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("EmploymentContractsList");
        }

        public IActionResult UpdateEmploymentContracts(int id)
        {
            var EmploymentContracts = _qtCtPmContext.EmploymentContracts.Where(t => t.ContractId == id).FirstOrDefault();
            var Employee = _qtCtPmContext.Employees.ToList();
            ViewBag.EmployeesSelectList = new SelectList(Employee, "EmployeeId", "LastName");
            return View(EmploymentContracts);
        }
        [HttpPost]
        public IActionResult UpdateEmploymentContracts(EmploymentContract EmploymentContracts)
        {
            _qtCtPmContext.EmploymentContracts.Update(EmploymentContracts);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("EmploymentContractsList");
        }
        public IActionResult DeleteEmploymentContracts(int id)
        {
            var EmploymentContracts = _qtCtPmContext.EmploymentContracts.Where(t => t.ContractId == id).FirstOrDefault();
            _qtCtPmContext.Remove(EmploymentContracts);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("EmploymentContractsList");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
