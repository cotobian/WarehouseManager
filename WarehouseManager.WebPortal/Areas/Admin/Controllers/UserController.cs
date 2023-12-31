﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Admin.User;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : BaseController<User>
    {
        public UserController(IConfiguration configuration, ILogger<WebPortal.Controllers.HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllUser()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<GetUserVm> list = JsonConvert.DeserializeObject<List<GetUserVm>>(responseBody).ToList();
            return Json(new { data = list });
        }

        [HttpGet]
        public async Task<ActionResult> AddOrEdit(int id = 0)
        {
            ViewBag.DepartmentList = (await DepartmentList()).Select(c => new { c.Id, c.Name });
            ViewBag.RoleList = (await RoleList()).Select(c => new { c.Id, c.Name });
            if (id == 0) return View(new User());
            else
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + "/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                User User = JsonConvert.DeserializeObject<User>(responseBody);
                return View(User);
            }
        }

        [HttpPost]
        public override async Task<JsonResult> AddOrEdit(User obj)
        {
            ViewBag.RoleList = (await RoleList()).Select(c => new { c.Id, c.Name });
            ViewBag.DepartmentList = (await DepartmentList()).Select(c => new { c.Id, c.Name });
            if (obj.RoleId == 0 || obj.DepartmentId == 0)
                return Json(new { success = false, message = "Hãy chọn chức danh và đơn vị!" });
            if (obj.Id == 0)
            {
                CreateUserVm userVm = new()
                {
                    FullName = obj.FullName,
                    Username = obj.Username,
                    DepartmentId = obj.DepartmentId,
                    RoleId = obj.RoleId,
                    Password = "123456"
                };
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl + "/RegisterUserVm", userVm);
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

        private async Task<List<Department>> DepartmentList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/Department");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<Department> departments = JsonConvert.DeserializeObject<List<Department>>(responseBody);
            return departments;
        }

        private async Task<List<Role>> RoleList()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/Role");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<Role> roles = JsonConvert.DeserializeObject<List<Role>>(responseBody);
            return roles;
        }
    }
}