using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Warehouse.Controllers
{
    [Area("Index")]
    public class WarehouseController : BaseController<BackendServer.Data.Entities.Warehouse>
    {
        public WarehouseController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllWarehouse()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<BackendServer.Data.Entities.Warehouse> list = JsonConvert.DeserializeObject<List<BackendServer.Data.Entities.Warehouse>>(responseBody).Where(c => c.Status == true).ToList();
            return Json(new { data = list });
        }

        [HttpGet]
        public async Task<ActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0) return View(new BackendServer.Data.Entities.Warehouse());
            else
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                BackendServer.Data.Entities.Warehouse warehouse = JsonConvert.DeserializeObject<BackendServer.Data.Entities.Warehouse>(responseBody);
                return View(warehouse);
            }
        }
    }
}