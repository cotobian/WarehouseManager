using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class ExtraReceiptBarcodeController : BaseController<ExtraReceiptBarcode>
    {
        public ExtraReceiptBarcodeController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}