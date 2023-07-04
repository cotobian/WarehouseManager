using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class WarehouseDoorsController : BaseController<WarehouseDoor>
    {
        public WarehouseDoorsController(WhContext context) : base(context)
        {
        }
    }
}