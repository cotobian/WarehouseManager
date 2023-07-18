using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class TallyJobController : BaseController<TallyJob>
    {
        public TallyJobController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllTallyJob()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<TallyJob> list = JsonConvert.DeserializeObject<List<TallyJob>>(responseBody).ToList();
            return Json(new { data = list });
        }

        [HttpGet]
        public async Task<ActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0) return View(new TallyJob());
            else
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                TallyJob TallyJob = JsonConvert.DeserializeObject<TallyJob>(responseBody);
                return View(TallyJob);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateByReceiptDetail(int detailId)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl + "/ReceiptDetail", detailId);
            if (response.IsSuccessStatusCode)
                return Json(new { success = true, message = "Tạo job thành công" });
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(errorContent))
                    return Json(new { success = false, message = "Có lỗi Tạo job" });
                else
                    return Json(new { success = false, message = errorContent });
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateByReceiptOrder(int orderId)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl + "/ReceiptOrder", orderId);
            if (response.IsSuccessStatusCode)
                return Json(new { success = true, message = "Tạo job thành công" });
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(errorContent))
                    return Json(new { success = false, message = "Có lỗi Tạo job" });
                else
                    return Json(new { success = false, message = errorContent });
            }
        }
    }
}