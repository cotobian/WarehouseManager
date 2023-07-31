using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Index;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Warehouse.Controllers
{
    [Area("Index")]
    public class WarehousePositionController : BaseController<WarehousePosition>
    {
        public WarehousePositionController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllWarehousePosition()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<GetWarehousePositionVm> list = JsonConvert.DeserializeObject<List<GetWarehousePositionVm>>(responseBody).Where(c => c.Status == true).ToList();
            return Json(new { data = list });
        }

        [HttpGet]
        public async Task<ActionResult> AddOrEdit(int id = 0)
        {
            ViewBag.WarehouseList = (await WarehouseList()).Select(c => new { c.Id, c.Name });
            if (id == 0) return View(new WarehousePosition());
            else
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                WarehousePosition WarehousePosition = JsonConvert.DeserializeObject<WarehousePosition>(responseBody);
                return View(WarehousePosition);
            }
        }

        private async Task<List<BackendServer.Data.Entities.Warehouse>> WarehouseList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/Warehouse");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<BackendServer.Data.Entities.Warehouse> warehouses = JsonConvert.DeserializeObject<List<BackendServer.Data.Entities.Warehouse>>(responseBody);
            return warehouses;
        }
    }
}