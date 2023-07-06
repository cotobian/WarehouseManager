using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolePermissionController : BaseController<RolePermission>
    {
        public RolePermissionController(IConfiguration configuration, ILogger<WebPortal.Controllers.HomeController> logger) : base(configuration, logger)
        {
        }

        private string apiUrl = "/api/RolePermissions";

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetRolePermissionById(int roleid)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + roleid);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<RolePermission> list = JsonConvert.DeserializeObject<List<RolePermission>>(responseBody).Where(c => c.Status == true).ToList();
            return Json(new { data = list });
        }
    }
}