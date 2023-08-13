using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;
using WarehouseManager.ViewModels.Warehouse.CurrentPosition;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class CurrentPositionController : BaseController<CurrentPosition>
    {
        public CurrentPositionController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.WarehouseList = (await WarehouseList()).Select(c => new { c.Id, c.Name });
            return View();
        }

        public async Task<IActionResult> StackLayout()
        {
            ViewBag.WarehouseList = (await WarehouseList()).Select(c => new { c.Id, c.Name });
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DisplayTier(string bay, string row, int warehouseid)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/StackLayout/" + warehouseid);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<DisplayTierVm> list = JsonConvert.DeserializeObject<List<DisplayTierVm>>(responseBody).ToList();
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllCurrentPosition()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<CurrentPosition> list = JsonConvert.DeserializeObject<List<CurrentPosition>>(responseBody).Where(c => c.Status != CurrentPositionStatus.Deleted).ToList();
            return Json(new { data = list });
        }

        [HttpGet]
        public async Task<JsonResult> GetStackLayout(int warehouseid)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/StackLayout/" + warehouseid);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<StackLayoutVm> list = JsonConvert.DeserializeObject<List<StackLayoutVm>>(responseBody).ToList();
            return Json(list);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0) return View(new CurrentPosition());
            else
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                CurrentPosition currentPosition = JsonConvert.DeserializeObject<CurrentPosition>(responseBody);
                return View(currentPosition);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetLayoutPosition(int warehouseid)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "PositionLayout/" + warehouseid);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<LayoutPositionVm> list = JsonConvert.DeserializeObject<List<LayoutPositionVm>>(responseBody).ToList();
            return Json(new { data = list });
        }

        private async Task<List<BackendServer.Data.Entities.Warehouse>> WarehouseList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/Warehouse");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<BackendServer.Data.Entities.Warehouse> roles = JsonConvert.DeserializeObject<List<BackendServer.Data.Entities.Warehouse>>(responseBody).Where(c => c.Status != false).ToList();
            return roles;
        }
    }
}