using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class WarehouseController : BaseController<Warehouse>
    {
        public WarehouseController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}