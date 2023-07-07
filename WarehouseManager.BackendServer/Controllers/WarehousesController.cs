using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class WarehousesController : BaseController<Warehouse>
    {
        public WarehousesController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}