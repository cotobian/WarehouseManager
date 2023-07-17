using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class ReceiptDetailController : BaseController<ReceiptDetail>
    {
        public ReceiptDetailController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index(int orderid = 0)
        {
            ViewBag.OrderId = orderid;
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllReceiptDetail()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<ReceiptDetail> list = JsonConvert.DeserializeObject<List<ReceiptDetail>>(responseBody).Where(c => c.Status != false).ToList();
            return Json(new { data = list });
        }

        [HttpGet]
        public async Task<ActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0) return View(new ReceiptDetail());
            else
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                ReceiptDetail ReceiptDetail = JsonConvert.DeserializeObject<ReceiptDetail>(responseBody);
                return View(ReceiptDetail);
            }
        }
    }
}