using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class CustomersController : BaseController<Customer>
    {
        public CustomersController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}