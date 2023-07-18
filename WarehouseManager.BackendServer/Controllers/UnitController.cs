using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class UnitController : BaseController<Unit>
    {
        public UnitController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}