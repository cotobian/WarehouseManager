using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class WarehousePositionsController : BaseController<WarehousePosition>
    {
        public WarehousePositionsController(WhContext context) : base(context)
        {
        }
    }
}