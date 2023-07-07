using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class ReceiptDetailsController : BaseController<ReceiptDetail>
    {
        public ReceiptDetailsController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}