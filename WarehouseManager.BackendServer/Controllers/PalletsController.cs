using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class PalletsController : BaseController<Pallet>
    {
        public PalletsController(WhContext context) : base(context)
        {
        }
    }
}