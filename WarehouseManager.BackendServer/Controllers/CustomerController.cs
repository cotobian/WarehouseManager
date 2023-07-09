using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class CustomerController : BaseController<Customer>
    {
        public CustomerController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}