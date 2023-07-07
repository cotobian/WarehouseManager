using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class ExtraReceiptBarcodesController : BaseController<ExtraReceiptBarcode>
    {
        public ExtraReceiptBarcodesController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}