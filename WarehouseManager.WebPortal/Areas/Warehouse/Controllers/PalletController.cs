using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class PalletController : BaseController<Pallet>
    {
        public PalletController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllPallet()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<Pallet> list = JsonConvert.DeserializeObject<List<Pallet>>(responseBody).Where(c => c.Status != false).ToList();
            return Json(new { data = list });
        }

        [HttpGet]
        public ActionResult AddOrEdit()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> AddOrEdit(int palletNo)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl + "/CreateList", palletNo);
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Tạo mới dữ liệu thành công" });
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(errorContent))
                    return Json(new { success = false, message = "Có lỗi tạo mới dữ liệu" });
                else
                    return Json(new { success = false, message = errorContent });
            }
        }
    }
}