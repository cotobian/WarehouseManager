using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class WarehouseDoorController : BaseController<WarehouseDoor>
    {
        public WarehouseDoorController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}