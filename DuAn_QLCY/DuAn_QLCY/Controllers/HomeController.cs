using DuAn_QLCY.Models;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult EmployeesList(int? page)
        {
            var Employees = _qtCtPmContext.Employees.Include(a => a.Department).Include(a => a.Type).ToPagedList(page ?? 1, 10);
            return View(Employees);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
