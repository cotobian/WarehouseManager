using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class ExtraReceiptDetailController : BaseController<ExtraReceiptDetail>
    {
        public ExtraReceiptDetailController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}