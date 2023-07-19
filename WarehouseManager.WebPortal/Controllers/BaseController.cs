using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace WarehouseManager.WebPortal.Controllers
{
    public class BaseController<T> : Controller where T : class
    {
        private readonly ILogger<HomeController> _logger;
        public readonly IConfiguration _configuration;
        public readonly HttpClient _httpClient;

        public BaseController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("ApiUrl"));
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _logger = logger;
        }

        public readonly string apiUrl = "/api/" + typeof(T).Name;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string accessToken = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(accessToken))
            {
                context.Result = new RedirectResult("/Auth/Login");
                return;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            base.OnActionExecuting(context);
        }

        [HttpPost]
        public virtual async Task<JsonResult> AddOrEdit(T obj)
        {
            PropertyInfo idProperty = typeof(T).GetProperty("Id");
            int? Id = (int?)idProperty.GetValue(obj);
            if (Id == 0 || Id == null)
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl, obj);
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
            else
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync(apiUrl + "/" + Id, obj);
                if (response.IsSuccessStatusCode)
                    return Json(new { success = true, message = "Cập nhật dữ liệu thành công" });
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

        [HttpGet]
        public virtual async Task<JsonResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(apiUrl + "/" + id);
            if (response.IsSuccessStatusCode)
                return Json(new { success = true, message = "Xóa dữ liệu thành công" });
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(errorContent))
                    return Json(new { success = false, message = "Có lỗi xóa dữ liệu" });
                else
                    return Json(new { success = false, message = errorContent });
            }
        }
    }
}