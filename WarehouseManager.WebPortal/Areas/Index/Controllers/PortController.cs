using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Warehouse.Controllers
{
    public class PortController : BaseController<Port>
    {
        public PortController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}