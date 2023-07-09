using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class WarehousePositionController : BaseController<WarehousePosition>
    {
        public WarehousePositionController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}