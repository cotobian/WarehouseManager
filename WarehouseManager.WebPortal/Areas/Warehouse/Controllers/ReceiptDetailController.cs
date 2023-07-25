using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;
using WarehouseManager.ViewModels.Warehouse.ReceiptDetail;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class ReceiptDetailController : BaseController<ReceiptDetail>
    {
        #region constructor

        public ReceiptDetailController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        #endregion constructor

        #region Public Methods

        public IActionResult Index(int orderid = 0)
        {
            ViewBag.OrderId = orderid;
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetReceiptDetailByOrder(int orderid)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + @"\orderid\" + orderid);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<GetReceiptDetailVm> list = JsonConvert.DeserializeObject<List<GetReceiptDetailVm>>(responseBody).ToList();
            return Json(new { data = list });
        }

        [HttpGet]
        public async Task<ActionResult> AddOrEdit(int id = 0)
        {
            ViewBag.UnitList = (await UnitList()).Select(c => new { c.Id, c.Name }).ToList();
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

        [HttpPost]
        public override async Task<JsonResult> AddOrEdit(ReceiptDetail obj)
        {
            obj.OrderId = ViewBag.OrderId;
            if (obj.Id == 0)
            {
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

        [HttpGet]
        public async Task<ActionResult> PrintSheet(int detailId = 0)
        {
            ViewBag.UnitList = (await UnitList()).Select(c => new { c.Id, c.Name }).ToList();
            if (detailId == 0) return View(new ReceiptDetail());
            else
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + detailId);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                ReceiptDetail ReceiptDetail = JsonConvert.DeserializeObject<ReceiptDetail>(responseBody);
                return View(ReceiptDetail);
            }
        }

        [HttpGet]
        public async Task<ActionResult> PrintAllSheet(int orderId = 0)
        {
            ViewBag.UnitList = (await UnitList()).Select(c => new { c.Id, c.Name }).ToList();
            if (orderId == 0) return View(new ReceiptDetail());
            else
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + orderId);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                ReceiptDetail ReceiptDetail = JsonConvert.DeserializeObject<ReceiptDetail>(responseBody);
                return View(ReceiptDetail);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private async Task<List<Unit>> UnitList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/Unit");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<Unit> units = JsonConvert.DeserializeObject<List<Unit>>(responseBody);
            return units;
        }

        private void GenerateExcelFile(string filePath)
        {
        }

        #endregion Private Methods
    }
}