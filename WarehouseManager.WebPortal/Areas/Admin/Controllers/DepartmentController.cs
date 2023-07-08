using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Admin.Department;
using WarehouseManager.WebPortal.Controllers;

namespace WarehouseManager.WebPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : BaseController<Department>
    {
        public DepartmentController(IConfiguration configuration, ILogger<WebPortal.Controllers.HomeController> logger) : base(configuration, logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetTreeView()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<TreeViewNode> list = ConvertToTreeView(JsonConvert.DeserializeObject<List<Department>>(responseBody).Where(c => c.Status == true).ToList());
            return Json(list);
        }

        // Hàm chuyển đổi danh sách các đối tượng Department thành cây
        public List<TreeViewNode> ConvertToTreeView(List<Department> departmentList)
        {
            var treeData = new List<TreeViewNode>();
            var departmentDict = departmentList.ToDictionary(d => d.Id, d => d);

            foreach (var department in departmentList)
            {
                if (department.ParentId == 0)
                {
                    var rootNode = new TreeViewNode
                    {
                        Id = department.Id,
                        text = department.Name,
                        nodes = new List<TreeViewNode>()
                    };

                    BuildTree(rootNode, departmentDict);
                    treeData.Add(rootNode);
                }
            }

            return treeData;
        }

        // Hàm xây dựng cây từ nút gốc
        private void BuildTree(TreeViewNode parentNode, Dictionary<int, Department> departmentDict)
        {
            if (departmentDict.ContainsKey(parentNode.Id))
            {
                var children = departmentDict.Values.Where(d => d.ParentId == parentNode.Id);

                foreach (var child in children)
                {
                    var childNode = new TreeViewNode
                    {
                        Id = child.Id,
                        text = child.Name,
                        nodes = new List<TreeViewNode>()
                    };

                    parentNode.nodes.Add(childNode);  // Thêm nút con vào nút cha

                    BuildTree(childNode, departmentDict);
                }
            }
        }
    }
}