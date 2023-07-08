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
        public RolePermissionController(IConfiguration configuration, ILogger<WebPortal.Controllers.HomeController> logger) : base(configuration, logger)
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
            List<RolePermissionVm> list = JsonConvert.DeserializeObject<List<RolePermissionVm>>(responseBody);
            return Json(new { data = list });
        }
    }
}