using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class DepartmentController : BaseController<Department>
    {
        public DepartmentController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}