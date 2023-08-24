using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Warehouse.PalletDetail;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class PalletDetailController : BaseController<PalletDetail>
    {
        public PalletDetailController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index(int palletId)
        {
            ViewBag.PalletId = palletId;
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetDetailByPallet(int palletId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + palletId);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<GetPalletDetailVm> list = JsonConvert.DeserializeObject<List<GetPalletDetailVm>>(responseBody).ToList();
            return Json(new { data = list });
        }
    }
}