using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FunctionController : BaseController<Function>
    {
        public FunctionController(IConfiguration configuration, ILogger<WebPortal.Controllers.HomeController> logger) : base(configuration, logger)
        {
        }

        private string apiUrl = "/api/Functions";

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllFunction()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<Function> list = JsonConvert.DeserializeObject<List<Function>>(responseBody).Where(c => c.Status == true).ToList();
            return Json(new { data = list });
        }

        [HttpGet]
        public async Task<ActionResult> AddOrEdit(int id = 0)
        {
            ViewBag.ParentList = (await GetParentList()).Select(c => new { c.Id, c.Name });
            if (id == 0) return View(new Function());
            else
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Function function = JsonConvert.DeserializeObject<Function>(responseBody);
                return View(function);
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddOrEdit(Function obj)
        {
            if (obj.Id == 0)
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl, obj);
                if (response.IsSuccessStatusCode)
                    return Json(new { success = true, message = "Tạo mới dữ liệu thành công" });
                else return Json(new { success = false, message = "Có lỗi tạo dữ liệu" });
            }
            else
            {
                var data = new { item = obj, id = obj.Id };
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync(apiUrl, data);
                if (response.IsSuccessStatusCode)
                    return Json(new { success = true, message = "Cập nhật dữ liệu thành công" });
                else return Json(new { success = false, message = "Có lỗi cập nhật dữ liệu" });
            }
        }

        private async Task<List<Function>> GetParentList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/Parent");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<Function> functions = JsonConvert.DeserializeObject<List<Function>>(responseBody);
            List<Function> parentFunction = functions.Where(c => c.ParentId == null).ToList();
            return parentFunction;
        }

        [HttpGet]
        public async Task<JsonResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(apiUrl + "/" + id);
            if (response.IsSuccessStatusCode)
                return Json(new { success = true, message = "Xóa dữ liệu thành công" });
            else return Json(new { success = false, message = "Có lỗi xóa dữ liệu" });
        }
    }
}