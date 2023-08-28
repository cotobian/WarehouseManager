using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;
using WarehouseManager.ViewModels.Warehouse.ForkliftJob;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class ForkliftJobController : BaseController<ForkliftJob>
    {
        public ForkliftJobController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllForkliftJob()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<GetForkliftJobVm> list = JsonConvert.DeserializeObject<List<GetForkliftJobVm>>(responseBody).ToList();
            return Json(new { data = list });
        }

        [HttpGet]
        public async Task<ActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0) return View(new ForkliftJob());
            else
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                ForkliftJob ForkliftJob = JsonConvert.DeserializeObject<ForkliftJob>(responseBody);
                return View(ForkliftJob);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateByDeliveryOrder(int orderId)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl + "/DeliveryOrder", orderId);
            if (response.IsSuccessStatusCode)
                return Json(new { success = true, message = "Tạo job thành công" });
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(errorContent))
                    return Json(new { success = false, message = "Có lỗi tạo job" });
                else
                    return Json(new { success = false, message = errorContent });
            }
        }
    }
}