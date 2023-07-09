using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class DeliveryOrderController : BaseController<DeliveryOrder>
    {
        public DeliveryOrderController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllDeliveryOrder()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<DeliveryOrder> list = JsonConvert.DeserializeObject<List<DeliveryOrder>>(responseBody).Where(c => c.OrderStatus != OrderStatus.Deleted).ToList();

            return Json(new { data = list });
        }

        [HttpGet]
        public async Task<ActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0) return View(new DeliveryOrder());
            else
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                DeliveryOrder DeliveryOrder = JsonConvert.DeserializeObject<DeliveryOrder>(responseBody);
                return View(DeliveryOrder);
            }
        }
    }
}