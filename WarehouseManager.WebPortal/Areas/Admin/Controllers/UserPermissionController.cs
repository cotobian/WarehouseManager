using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Admin.UserPermission;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserPermissionController : BaseController<UserPermission>
    {
        public UserPermissionController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetUserPermissionByUserId(int userid)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/User/" + userid);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<GetUserPermissionVm> list = JsonConvert.DeserializeObject<List<GetUserPermissionVm>>(responseBody);
            return Json(new { data = list });
        }

        [HttpPost]
        public async Task<JsonResult> CreateUserPermissionForUser([FromBody] List<CreateUserPermissionVm> list)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl + "/User/", list);
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Cập nhật dữ liệu thành công" });
            }
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
}