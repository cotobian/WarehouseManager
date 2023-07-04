using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class ExtraReceiptDetailsController : BaseController<ExtraReceiptDetail>
    {
        public ExtraReceiptDetailsController(WhContext context) : base(context)
        {
        }
    }
}