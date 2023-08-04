using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.Runtime.InteropServices;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.WebPortal.Controllers;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace WarehouseManager.WebPortal.Areas.Warehouse.Controllers
{
    [Area("Warehouse")]
    public class PalletController : BaseController<Pallet>
    {
        public PalletController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        private string GenerateBarcode(string content)
        {
            try
            {
                BarcodeWriterPixelData barcodeWriter = new BarcodeWriterPixelData();
                EncodingOptions encodingOptions = new EncodingOptions();
                encodingOptions.Height = 300;
                encodingOptions.Width = 600;
                barcodeWriter.Options = encodingOptions;
                barcodeWriter.Format = BarcodeFormat.CODE_128;

                // Tạo hình ảnh mã vạch dưới dạng mảng byte
                PixelData pixelData = barcodeWriter.Write(content);

                // Tạo Bitmap từ dữ liệu pixel
                using (Bitmap barcodeImage = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb))
                {
                    BitmapData bitmapData = barcodeImage.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
                    Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    barcodeImage.UnlockBits(bitmapData);

                    // Lưu Bitmap thành tệp ảnh
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Content", "forms", "images", content + ".png");
                    barcodeImage.Save(imagePath, ImageFormat.Png);

                    // Trả về đường dẫn tới tệp ảnh
                    return imagePath;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
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
        public async Task<IActionResult> PrintMultiplePallet(string palletIds)
        {
            try
            {
                List<string> fileNames = new List<string>();
                List<byte[]> fileData = new List<byte[]>();
                string formsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Content", "forms");
                string filePath = Path.Combine(formsPath, "PALLETSHEET.xlsx");
                string imagePath = Path.Combine(formsPath, "images");
                string palletPath = Path.Combine(formsPath, "palletsheets");

                string[] palletsheets = Directory.GetFiles(palletPath);
                foreach (string file in palletsheets)
                {
                    System.IO.File.Delete(file);
                }

                string[] images = Directory.GetFiles(imagePath);
                foreach (string img in images)
                {
                    System.IO.File.Delete(img);
                }

                foreach (string pallet in palletIds.Split(','))
                {
                    string excelPath = Path.Combine(formsPath, "palletsheets", pallet + ".xlsx");
                    System.IO.File.Copy(filePath, excelPath);
                    FileInfo fileInfo = new FileInfo(excelPath);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage(fileInfo))
                    {
                        var workSheet = package.Workbook.Worksheets[0];
                        workSheet.Cells["E1"].Value = pallet;
                        string imgPath = GenerateBarcode(pallet);
                        var picture = workSheet.Drawings.AddPicture(pallet, imgPath);
                        picture.SetPosition(1, 0, 2, 0);
                        package.Save();
                        fileData.Add(package.GetAsByteArray());
                    }
                    fileNames.Add(pallet + ".xlsx");
                }

                var zipStream = new MemoryStream();
                var zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Create);
                for (int i = 0; i < fileNames.Count; i++)
                {
                    var entry = zipArchive.CreateEntry(fileNames[i]);
                    using var entryStream = entry.Open();
                    entryStream.Write(fileData[i], 0, fileData[i].Length);
                }
                zipStream.Position = 0;
                return File(zipStream, "application/zip", "ExcelFiles.zip");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}