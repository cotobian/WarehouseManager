using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Admin.Department;

namespace WarehouseManager.BackendServer.Controllers
{
    public class DepartmentController : BaseController<Department>
    {
        public DepartmentController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        private List<GetDepartmentVm> CreateDepartmentTree(List<Department> departments)
        {
            var departmentVms = new List<GetDepartmentVm>();

            // Tạo một từ điển để lưu trữ danh sách các nút cha của từng nút
            var parentDictionary = departments.ToLookup(d => d.ParentId ?? 0);

            // Hàm đệ quy để xây dựng cây dạng traverse và tính Level
            void TraverseDepartment(Department department, int level)
            {
                var departmentVm = new GetDepartmentVm
                {
                    Id = department.Id,
                    ParentId = department.ParentId ?? 0,
                    Name = department.Name,
                    Level = level
                };

                departmentVms.Add(departmentVm);

                foreach (var childDepartment in parentDictionary[department.Id])
                {
                    TraverseDepartment(childDepartment, level + 1);
                }
            }

            // Bắt đầu xây dựng cây từ gốc (ParentId = 0)
            foreach (var rootDepartment in parentDictionary[0])
            {
                TraverseDepartment(rootDepartment, 0);
            }

            return departmentVms;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAll()
        {
            List<Department> departments = await _context.Departments.Where(c => c.Status == true).OrderBy(c => c.Id).ToListAsync();
            var result = CreateDepartmentTree(departments);
            return Ok(result);
        }
    }
}