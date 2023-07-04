using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.WebPortal.Controllers
{
    public class HomeController : BaseController<User>
    {
        public HomeController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}