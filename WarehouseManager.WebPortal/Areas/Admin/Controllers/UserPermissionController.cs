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
        public UserPermissionController(IConfiguration configuration, ILogger<WebPortal.Controllers.HomeController> logger) : base(configuration, logger)
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
            List<UserPermissionVm> list = JsonConvert.DeserializeObject<List<UserPermissionVm>>(responseBody);
            return Json(new { data = list });
        }
    }
}