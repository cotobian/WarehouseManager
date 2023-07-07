using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class PortsController : BaseController<Port>
    {
        public PortsController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}