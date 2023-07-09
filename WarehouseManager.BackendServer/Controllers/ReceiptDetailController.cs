using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class ReceiptDetailController : BaseController<ReceiptDetail>
    {
        public ReceiptDetailController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}