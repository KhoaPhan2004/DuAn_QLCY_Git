using DuAn_QLCY.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList.Extensions;
using Task = DuAn_QLCY.Models.Task;

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
        public IActionResult DetailEmployees(int id)
        {
           
            var employeeDetails = _qtCtPmContext.Employees
                .Include(e => e.Department) 
                .Include(e => e.Type)     
                .Include(e => e.EmploymentContracts) 
                .Include(e => e.Salary)  
                .Include(e => e.PerformanceKpis) 
                .Include(e => e.Projects) 
                .Include(e => e.Tasks)     
                .Include(e => e.TimeTrackings)
                .Include(e => e.EmployeeTechnologies) 
                .ThenInclude(et => et.Tech)
                .Include(e => e.EmployeeTrainings)  
                .ThenInclude(et => et.Program) 
                .Include(e => e.Insurances) 
                .Include(e => e.Leaves)    
                .Include(e => e.Users) 
                .FirstOrDefault(e => e.EmployeeId == id); 

            if (employeeDetails == null)
            {
                return NotFound();
            }

            return View(employeeDetails);
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
            var Employee = _qtCtPmContext.Employees.ToList();
            ViewBag.EmployeesCTSelectList = new SelectList(Employee, "EmployeeId", "LastName");
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
            ViewBag.EmployeesCTSelectList = new SelectList(Employee, "EmployeeId", "LastName");
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






        public IActionResult ClientsList()
        {
            var Clients = _qtCtPmContext.Clients.ToList();
            return View(Clients);
        }
        public IActionResult CreateClients()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateClients(Client Clients)
        {
            _qtCtPmContext.Clients.Add(Clients);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("ClientsList");
        }
        public IActionResult UpdateClients(int id)
        {
            var Clients = _qtCtPmContext.Clients.Where(t => t.ClientId == id).FirstOrDefault();
            return View(Clients);
        }
        [HttpPost]
        public IActionResult UpdateClients(Client Clients)
        {
            _qtCtPmContext.Clients.Update(Clients);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("ClientsList");
        }

        public IActionResult DeleteClients(int id)
        {
            var Clients = _qtCtPmContext.Clients.Where(t => t.ClientId == id).FirstOrDefault();
            _qtCtPmContext.Remove(Clients);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("ClientsList");
        }








        public IActionResult TasksList(int? page)
        {
            var Tasks = _qtCtPmContext.Tasks.Include(a => a.Project).Include(a => a.AssignedToNavigation).ToPagedList(page ?? 1, 10);
            return View(Tasks);
        }
        public IActionResult CreateTasks()
        {
            var Projects = _qtCtPmContext.Projects.ToList();
            var Employees = _qtCtPmContext.Employees.ToList();

            ViewBag.ProjectsSelectList = new SelectList(Projects, "ProjectId", "ProjectName");
            ViewBag.EmployeesSelectList = new SelectList(Employees, "EmployeeId", "LastName");

            return View();
        }

        [HttpPost]
        public IActionResult CreateTasks(Task task)
        {
            _qtCtPmContext.Tasks.Add(task);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("TasksList");
        }

        public IActionResult UpdateTasks(int id)
        {
            var tasks = _qtCtPmContext.Tasks.Where(t => t.TaskId == id).FirstOrDefault();

            var Projects = _qtCtPmContext.Projects.ToList();
            var Employees = _qtCtPmContext.Employees.ToList();

            ViewBag.ProjectsSelectList = new SelectList(Projects, "ProjectId", "ProjectName");
            ViewBag.EmployeesSelectList = new SelectList(Employees, "EmployeeId", "LastName");

            return View(tasks);
        }
        [HttpPost]
        public IActionResult UpdateTasks(Task Task)
        {
            _qtCtPmContext.Tasks.Update(Task);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("TasksList");
        }
        public IActionResult DeleteTasks(int id)
        {
            var task = _qtCtPmContext.Tasks.Where(t => t.TaskId == id).FirstOrDefault();
            _qtCtPmContext.Remove(task);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("TasksList");
        }







        public IActionResult TechnologiesList()
        {
            var Technologies = _qtCtPmContext.Technologies.ToList();
            return View(Technologies);
        }
        public IActionResult CreateTechnologies()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTechnologies(Technology Technologies)
        {
            _qtCtPmContext.Technologies.Add(Technologies);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("TechnologiesList");
        }
        public IActionResult UpdateTechnologies(int id)
        {
            var Technologies = _qtCtPmContext.Technologies.Where(t => t.TechId == id).FirstOrDefault();
            return View(Technologies);
        }
        [HttpPost]
        public IActionResult UpdateTechnologies(Technology Technologies)
        {
            _qtCtPmContext.Technologies.Update(Technologies);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("TechnologiesList");
        }

        public IActionResult DeleteTechnologies(int id)
        {
            var Technologies = _qtCtPmContext.Technologies.Where(t => t.TechId == id).FirstOrDefault();
            _qtCtPmContext.Remove(Technologies);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("TechnologiesList");
        }








        public IActionResult EmployeeTechnologiesList(int? page)
        {
            var EmployeeTechnologies = _qtCtPmContext.EmployeeTechnologies.Include(a => a.Employee).Include(a => a.Tech).ToPagedList(page ?? 1, 10);
            return View(EmployeeTechnologies);
        }
        public IActionResult CreateEmployeeTechnologies()
        {
            var Employees = _qtCtPmContext.Employees.ToList();
            var Technologies = _qtCtPmContext.Technologies.ToList();

            ViewBag.EmployeesSelectList = new SelectList(Employees, "EmployeeId", "LastName");
            ViewBag.TechnologiesSelectList = new SelectList(Technologies, "TechId", "TechName");

            return View();
        }

        [HttpPost]
        public IActionResult CreateEmployeeTechnologies(EmployeeTechnology EmployeeTechnology)
        {
            try
            {
                _qtCtPmContext.EmployeeTechnologies.Add(EmployeeTechnology);
                _qtCtPmContext.SaveChanges();

                return RedirectToAction("EmployeeTechnologiesList");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Lỗi: Đã tồn tại thông tin công nghệ này cho nhân viên.");
                var Employees = _qtCtPmContext.Employees.ToList();
                var Technologies = _qtCtPmContext.Technologies.ToList();

                ViewBag.EmployeesSelectList = new SelectList(Employees, "EmployeeId", "LastName");
                ViewBag.TechnologiesSelectList = new SelectList(Technologies, "TechId", "TechName");
                return View();
            }
           
        }

        public IActionResult UpdateEmployeeTechnologies(int id,int id2)
        {
            var EmployeeTechnology = _qtCtPmContext.EmployeeTechnologies.Where(t => t.EmployeeId == id && t.TechId == id2).FirstOrDefault();

            

            return View(EmployeeTechnology);
        }
        [HttpPost]
        public IActionResult UpdateEmployeeTechnologies(EmployeeTechnology EmployeeTechnology)
        {
            _qtCtPmContext.EmployeeTechnologies.Update(EmployeeTechnology);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("EmployeeTechnologiesList");
        }
        public IActionResult DeleteEmployeeTechnologies(int id,int id2)
        {
            var EmployeeTechnology = _qtCtPmContext.EmployeeTechnologies.Where(t => t.EmployeeId == id && t.TechId == id2).FirstOrDefault();
            _qtCtPmContext.Remove(EmployeeTechnology);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("EmployeeTechnologiesList");
        }








        public IActionResult TrainingProgramsList()
        {
            var TrainingPrograms = _qtCtPmContext.TrainingPrograms.ToList();
            return View(TrainingPrograms);
        }
        public IActionResult CreateTrainingPrograms()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTrainingPrograms(TrainingProgram TrainingProgram)
        {
            _qtCtPmContext.TrainingPrograms.Add(TrainingProgram);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("TrainingProgramsList");
        }
        public IActionResult UpdateTrainingPrograms(int id)
        {
            var TrainingPrograms = _qtCtPmContext.TrainingPrograms.Where(t => t.ProgramId == id).FirstOrDefault();
            return View(TrainingPrograms);
        }
        [HttpPost]
        public IActionResult UpdateTrainingPrograms(TrainingProgram TrainingProgram)
        {
            _qtCtPmContext.TrainingPrograms.Update(TrainingProgram);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("TrainingProgramsList");
        }

        public IActionResult DeleteTrainingPrograms(int id)
        {
            var TrainingProgram = _qtCtPmContext.TrainingPrograms.Where(t => t.ProgramId == id).FirstOrDefault();
            _qtCtPmContext.Remove(TrainingProgram);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("TrainingProgramsList");
        }









        public IActionResult EmployeeTrainingsList(int? page)
        {
            var EmployeeTrainings = _qtCtPmContext.EmployeeTrainings.Include(a => a.Employee).Include(a => a.Program).ToPagedList(page ?? 1, 10);
            return View(EmployeeTrainings);
        }
        public IActionResult CreateEmployeeTrainings()
        {
            var Employees = _qtCtPmContext.Employees.ToList();
            var TrainingPrograms = _qtCtPmContext.TrainingPrograms.ToList();

            ViewBag.EmployeesSelectList = new SelectList(Employees, "EmployeeId", "LastName");
            ViewBag.TrainingProgramsSelectList = new SelectList(TrainingPrograms, "ProgramId", "ProgramName");

            return View();
        }

        [HttpPost]
        public IActionResult CreateEmployeeTrainings(EmployeeTraining EmployeeTraining)
        {
            try
            {
                _qtCtPmContext.EmployeeTrainings.Add(EmployeeTraining);
                _qtCtPmContext.SaveChanges();

                return RedirectToAction("EmployeeTrainingsList");
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "Lỗi: Đã tồn tại thông tin đào tạo này cho nhân viên.");
                var Employees = _qtCtPmContext.Employees.ToList();
                var TrainingPrograms = _qtCtPmContext.TrainingPrograms.ToList();
                ViewBag.EmployeesSelectList = new SelectList(Employees, "EmployeeId", "LastName");
                ViewBag.TrainingProgramsSelectList = new SelectList(TrainingPrograms, "ProgramId", "ProgramName");
                return View();
            }
                
        }

        public IActionResult UpdateEmployeeTrainings(int id, int id2)
        {
            var EmployeeTrainings = _qtCtPmContext.EmployeeTrainings.Where(t => t.EmployeeId == id && t.ProgramId == id2).FirstOrDefault();



            return View(EmployeeTrainings);
        }
        [HttpPost]
        public IActionResult UpdateEmployeeTrainings(EmployeeTraining EmployeeTraining)
        {
            _qtCtPmContext.EmployeeTrainings.Update(EmployeeTraining);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("EmployeeTrainingsList");
        }
        public IActionResult DeleteEmployeeTrainings(int id, int id2)
        {
            var EmployeeTechnology = _qtCtPmContext.EmployeeTrainings.Where(t => t.EmployeeId == id && t.ProgramId == id2).FirstOrDefault();
            _qtCtPmContext.Remove(EmployeeTechnology);
            _qtCtPmContext.SaveChanges();
            return RedirectToAction("EmployeeTrainingsList");
        }













        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
