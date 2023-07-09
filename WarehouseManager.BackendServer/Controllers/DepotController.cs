using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class DepotController : BaseController<Depot>
    {
        public DepotController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}