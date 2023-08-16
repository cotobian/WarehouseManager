using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
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
        public async Task<ActionResult> AddOrEdit(int orderId, int id = 0)
        {
            ViewBag.UnitList = (await UnitList()).Select(c => new { c.Id, c.Name }).ToList();
            ViewBag.OrderId = orderId;
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

        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile excelFile, int orderId)
        {
            if (excelFile != null && excelFile.Length > 0)
            {
                List<List<string>> uploadedData = new List<List<string>>();
                using (var stream = excelFile.OpenReadStream())
                using (var package = new ExcelPackage(stream))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    var worksheet = package.Workbook.Worksheets[0];
                    for (int row = 2; row <= worksheet.Dimension.Columns; row++)
                    {
                        List<string> rowData = new List<string>();
                        for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                        {
                            rowData.Add(worksheet.Cells[row, col].Value?.ToString() ?? "");
                        }
                        rowData.Add(orderId.ToString());
                        uploadedData.Add(rowData);
                    }
                }
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl + "/UploadExcel", uploadedData);
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Tạo mới dữ liệu thành công" });
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(errorContent))
                        return Json(new { success = false, message = "Có lỗi tạo mới dữ liệu" });
                    else return Json(new { success = false, message = errorContent });
                }
            }
            else return Json(new { success = false, message = "File Excel không có dữ liệu" });
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

        #endregion Private Methods
    }
}