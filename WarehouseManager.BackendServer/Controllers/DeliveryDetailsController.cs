using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class DeliveryDetailsController : BaseController<DeliveryDetail>
    {
        public DeliveryDetailsController(WhContext context) : base(context)
        {
        }
    }
}