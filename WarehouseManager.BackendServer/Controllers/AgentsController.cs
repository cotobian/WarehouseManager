using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class AgentsController : BaseController<Agent>
    {
        public AgentsController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}