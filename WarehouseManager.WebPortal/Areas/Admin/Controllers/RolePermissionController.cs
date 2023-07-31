using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Admin.RolePermission;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolePermissionController : BaseController<RolePermission>
    {
        public RolePermissionController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetRolePermissionByRoleId(int roleid)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/Role/" + roleid);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<GetRolePermissionVm> list = JsonConvert.DeserializeObject<List<GetRolePermissionVm>>(responseBody);
            return Json(new { data = list });
        }

        [HttpPost]
        public async Task<JsonResult> CreateRolePermissionForRole(List<CreateRolePermissionVm> list)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl + "/Role/", list);
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