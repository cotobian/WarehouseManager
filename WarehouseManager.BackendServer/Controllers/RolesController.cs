using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class RolesController : BaseController<Role>
    {
        public RolesController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}