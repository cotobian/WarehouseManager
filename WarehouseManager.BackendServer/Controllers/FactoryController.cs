using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class FactoryController : BaseController<Factory>
    {
        public FactoryController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}