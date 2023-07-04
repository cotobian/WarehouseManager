using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class FactoriesController : BaseController<Factory>
    {
        public FactoriesController(WhContext context) : base(context)
        {
        }
    }
}