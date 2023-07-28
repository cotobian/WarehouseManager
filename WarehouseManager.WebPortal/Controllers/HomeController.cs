using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.WebPortal.Controllers
{
    public class HomeController : BaseController<User>
    {
        public HomeController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}