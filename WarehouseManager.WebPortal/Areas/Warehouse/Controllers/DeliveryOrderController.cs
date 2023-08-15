using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;
using WarehouseManager.ViewModels.Warehouse.DeliveryOrder;
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
            List<GetDeliveryOrderVm> list = JsonConvert.DeserializeObject<List<GetDeliveryOrderVm>>(responseBody).ToList();
            return Json(new { data = list });
        }

        [HttpGet]
        public async Task<ActionResult> AddOrEdit(int id = 0)
        {
            ViewBag.WarehouseDoorList = await WarehouseDoorList();
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

        [HttpPost]
        public override async Task<JsonResult> AddOrEdit(DeliveryOrder obj)
        {
            if (obj.Id == 0)
            {
                obj.OrderNo = "0";
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl, obj);
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
            else
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync(apiUrl + "/" + obj.Id, obj);
                if (response.IsSuccessStatusCode)
                    return Json(new { success = true, message = "Cập nhật dữ liệu thành công" });
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(errorContent))
                        return Json(new { success = false, message = "Có lỗi cập nhật dữ liệu" });
                    else
                        return Json(new { success = false, message = errorContent });
                }
            }
        }

        private async Task<List<WarehouseDoor>> WarehouseDoorList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/WarehouseDoor");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<WarehouseDoor> agents = JsonConvert.DeserializeObject<List<WarehouseDoor>>(responseBody);
            return agents;
        }
    }
}