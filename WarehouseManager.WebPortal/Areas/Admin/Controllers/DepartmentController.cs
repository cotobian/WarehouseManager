using Microsoft.AspNetCore.Mvc;

namespace WarehouseManager.WebPortal.Areas.Admin.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
