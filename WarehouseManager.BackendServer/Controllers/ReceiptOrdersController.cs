using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class ReceiptOrdersController : BaseController<ReceiptOrder>
    {
        public ReceiptOrdersController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}