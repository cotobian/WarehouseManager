using Microsoft.AspNetCore.Mvc;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Warehouse.Controllers
{
    public class WarehouseController : BaseController<BackendServer.Data.Entities.Warehouse>
    {
        public WarehouseController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}