using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;
using WarehouseManager.ViewModels.Warehouse.ReceiptOrder;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class ReceiptOrderController : BaseController<ReceiptOrder>
    {
        public ReceiptOrderController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllReceiptOrder()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<GetReceiptOrderVm> list = JsonConvert.DeserializeObject<List<GetReceiptOrderVm>>(responseBody).ToList();
            return Json(new { data = list });
        }

        [HttpGet]
        public async Task<ActionResult> AddOrEdit(int id = 0)
        {
            ViewBag.CustomerList = (await CustomerList()).Where(c => c.Status != false).Select(c => new { c.Id, c.Name });
            ViewBag.AgentList = (await AgentList()).Where(c => c.Status != false).Select(c => new { c.Id, c.Name });
            if (id == 0) return View(new ReceiptOrder());
            else
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                ReceiptOrder ReceiptOrder = JsonConvert.DeserializeObject<ReceiptOrder>(responseBody);
                return View(ReceiptOrder);
            }
        }

        [HttpPost]
        public override async Task<JsonResult> AddOrEdit(ReceiptOrder obj)
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

        private async Task<List<Agent>> AgentList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/Agent");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<Agent> agents = JsonConvert.DeserializeObject<List<Agent>>(responseBody);
            return agents;
        }

        private async Task<List<Customer>> CustomerList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/Customer");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(responseBody);
            return customers;
        }
    }
}