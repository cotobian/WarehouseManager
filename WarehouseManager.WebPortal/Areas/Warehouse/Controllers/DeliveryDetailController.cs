using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;
using WarehouseManager.ViewModels.Warehouse.DeliveryDetail;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class DeliveryDetailController : BaseController<DeliveryDetail>
    {
        public DeliveryDetailController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index(int orderid = 0)
        {
            ViewBag.OrderId = orderid;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> AddOrEdit(int orderId, int id = 0)
        {
            ViewBag.POList = await GetPOList();
            ViewBag.ItemList = await GetItemList();
            ViewBag.OrderId = orderId;
            if (id == 0) return View(new GetDeliveryDetailVm());
            else
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                GetDeliveryDetailVm DeliveryDetail = JsonConvert.DeserializeObject<GetDeliveryDetailVm>(responseBody);
                return View(DeliveryDetail);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetDeliveryDetailByOrder(int orderid)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/orderid/" + orderid);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<GetDeliveryDetailVm> list = JsonConvert.DeserializeObject<List<GetDeliveryDetailVm>>(responseBody).ToList();
            return Json(new { data = list });
        }

        [HttpPost]
        public async Task<JsonResult> AddOrEditVm(GetDeliveryDetailVm obj)
        {
            if (obj.Id == 0)
            {
                obj.Status = true;
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl + "/AddDeliveryVm/", obj);
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Tạo mới dữ liệu thành công" });
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(errorContent)) return Json(new { success = false, message = "Có lỗi tạo mới dữ liệu" });
                    else return Json(new { success = false, message = errorContent });
                }
            }
            else
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync(apiUrl + "/" + obj.Id, convertGetVm(obj));
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

        #region Private

        private DeliveryDetail convertGetVm(GetDeliveryDetailVm vm)
        {
            return new DeliveryDetail
            {
                Id = vm.Id,
                DeliveryOrderId = vm.DeliveryOrderId,
                ReceiptDetailId = vm.ReceiptDetailId,
                Quantity = vm.Quantity,
                PositionId = vm.PositionId,
                Size = vm.Size,
                CBM = vm.CBM,
                Weight = vm.Weight,
                Status = vm.Status
            };
        }

        private async Task<List<string>> GetPOList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/ReceiptDetail/getStackedPO");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<string> pos = JsonConvert.DeserializeObject<List<string>>(responseBody);
            return pos;
        }

        private async Task<List<string>> GetItemList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/ReceiptDetail/getStackedItem");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<string> items = JsonConvert.DeserializeObject<List<string>>(responseBody);
            return items;
        }

        #endregion Private
    }
}