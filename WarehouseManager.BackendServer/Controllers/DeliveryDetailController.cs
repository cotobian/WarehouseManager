using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class DeliveryDetailController : BaseController<DeliveryDetail>
    {
        public DeliveryDetailController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}