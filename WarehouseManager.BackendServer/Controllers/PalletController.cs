using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class PalletController : BaseController<Pallet>
    {
        public PalletController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}