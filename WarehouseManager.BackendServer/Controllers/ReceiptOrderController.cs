using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class ReceiptOrderController : BaseController<ReceiptOrder>
    {
        public ReceiptOrderController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}