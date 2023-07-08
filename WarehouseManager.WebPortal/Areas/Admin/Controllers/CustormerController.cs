using Microsoft.AspNetCore.Mvc;

namespace WarehouseManager.WebPortal.Areas.Admin.Controllers
{
    public class CustormerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}