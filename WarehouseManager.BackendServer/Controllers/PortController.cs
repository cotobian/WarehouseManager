using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class PortController : BaseController<Port>
    {
        public PortController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}