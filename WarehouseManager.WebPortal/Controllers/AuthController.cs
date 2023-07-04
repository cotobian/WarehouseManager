using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WarehouseManager.ViewModels;

namespace WarehouseManager.WebPortal.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly IConfiguration _configuration;
        public readonly HttpClient _httpClient;

        public AuthController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("ApiUrl"));
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVm user)
        {
            var data = new { Username = user.Username, Password = user.Password };
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/Auth/Login", data);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                string token = JsonConvert.DeserializeObject<string>(result);
                if (token != null)
                {
                    HttpContext.Session.SetString("JwtToken", token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }
    }
}