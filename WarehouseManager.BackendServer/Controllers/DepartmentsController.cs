using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class DepartmentsController : BaseController<Department>
    {
        public DepartmentsController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}