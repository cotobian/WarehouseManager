using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Drawing;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.WebPortal.Controllers;
using ZXing;
using ZXing.Common;

namespace WarehouseManager.WebPortal.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class PalletController : BaseController<Pallet>
    {
        public PalletController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public static Bitmap GenerateBarcode(string content)
        {
            BarcodeWriter<Bitmap> barcodeWriter = new BarcodeWriter<Bitmap>();
            EncodingOptions encodingOptions = new EncodingOptions();
            encodingOptions.Height = 300;
            encodingOptions.Width = 600;
            barcodeWriter.Options = encodingOptions;
            barcodeWriter.Format = BarcodeFormat.CODE_128;
            Bitmap barcodeImage = barcodeWriter.Write(content);
            return barcodeImage;
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
        public async Task<JsonResult> AddNumber(int palletNo)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/CreateList/" + palletNo);
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

        [HttpGet]
        public async Task<IActionResult> PrintSinglePallet(int palletId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + palletId);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Pallet pallet = JsonConvert.DeserializeObject<Pallet>(responseBody);
            string templateFilePath = Path.Combine(Directory.GetCurrentDirectory(), "forms", "PALLETSHEET.xlsx");

            using (var package = new ExcelPackage(new FileInfo(templateFilePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                worksheet.Cells["E1"].Value = pallet.PalletNo;
                Bitmap barcodeImage = GenerateBarcode(pallet.PalletNo);
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "forms/palletsheets");
                var modifiedFileContents = package.GetAsByteArray();
                return File(modifiedFileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "modified.xlsx");
            }
        }

        [HttpGet]
        public async Task<IActionResult> PrintMultiplePallet(string palletIds)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + palletIds);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Pallet pallet = JsonConvert.DeserializeObject<Pallet>(responseBody);
            string templateFilePath = Path.Combine(Directory.GetCurrentDirectory(), "forms", "PALLETSHEET.xlsx");

            using (var package = new ExcelPackage(new FileInfo(templateFilePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                // Thay đổi dữ liệu trong file Excel
                worksheet.Cells["A2"].Value = "New Data1";
                worksheet.Cells["B2"].Value = "New Data2";

                // Lưu lại file Excel sau khi sửa đổi
                var modifiedFileContents = package.GetAsByteArray();
                return File(modifiedFileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "modified.xlsx");
            }
        }
    }
}