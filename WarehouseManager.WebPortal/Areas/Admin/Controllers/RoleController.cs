using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : BaseController<Role>
    {
        public RoleController(IConfiguration configuration, ILogger<WebPortal.Controllers.HomeController> logger) : base(configuration, logger)
        {
        }

        private string apiUrl = "/api/Roles";

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllRole()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<Role> list = JsonConvert.DeserializeObject<List<Role>>(responseBody).Where(c => c.Status == true).ToList();
            return Json(new { data = list });
        }

        [HttpGet]
        public async Task<ActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0) return View(new Role());
            else
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Role Role = JsonConvert.DeserializeObject<Role>(responseBody);
                return View(Role);
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddOrEdit(Role obj)
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