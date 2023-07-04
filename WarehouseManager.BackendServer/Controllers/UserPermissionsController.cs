using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class UserPermissionsController : BaseController<UserPermission>
    {
        public UserPermissionsController(WhContext context) : base(context)
        {
        }
    }
}