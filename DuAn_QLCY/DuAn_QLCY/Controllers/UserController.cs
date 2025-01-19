using DuAn_QLCY.Models;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_QLCY.Controllers
{
    
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly QlCtPmContext _qtCtPmContext;

        public UserController(ILogger<UserController> logger, QlCtPmContext qtCtPmContext)
        {
            _logger = logger;
            _qtCtPmContext = qtCtPmContext;
        }
        public IActionResult Index()
        {
            return View();
        }

      

    }
}
