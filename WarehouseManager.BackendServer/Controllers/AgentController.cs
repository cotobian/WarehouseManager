using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class AgentController : BaseController<Agent>
    {
        public AgentController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}