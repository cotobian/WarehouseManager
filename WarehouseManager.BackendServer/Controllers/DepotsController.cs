using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepotsController : BaseController<Depot>
    {
        public DepotsController(WhContext context) : base(context)
        {
        }
    }
}