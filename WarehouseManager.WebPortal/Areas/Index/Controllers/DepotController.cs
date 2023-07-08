using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Warehouse.Controllers
{
    [Area("Index")]
    public class DepotController : BaseController<Depot>
    {
        public DepotController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllDepot()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<Depot> list = JsonConvert.DeserializeObject<List<Depot>>(responseBody).Where(c => c.Status == true).ToList();
            return Json(new { data = list });
        }

        [HttpGet]
        public async Task<ActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0) return View(new Depot());
            else
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Depot depot = JsonConvert.DeserializeObject<Depot>(responseBody);
                return View(depot);
            }
        }
    }
}