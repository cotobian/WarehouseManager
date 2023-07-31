using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Admin.Department;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : BaseController<Department>
    {
        public DepartmentController(IConfiguration configuration, ILogger<HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllDepartment()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<GetDepartmentVm> list = JsonConvert.DeserializeObject<List<GetDepartmentVm>>(responseBody).ToList();
            return Json(new { data = list });
        }

    }
}