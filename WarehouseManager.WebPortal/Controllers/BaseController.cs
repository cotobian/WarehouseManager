using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;

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
    }
}