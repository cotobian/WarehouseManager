using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Index.WarehouseDoor;

namespace WarehouseManager.BackendServer.Controllers
{
    public class WarehouseDoorController : BaseController<WarehouseDoor>
    {
        public WarehouseDoorController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        [HttpGet]
        public override async Task<IActionResult> GetAll()
        {
            var query = from wd in _context.WarehouseDoors
                        join w in _context.Warehouses on wd.WarehouseId equals w.Id
                        where wd.Status == true && w.Status == true
                        select new GetWarehouseDoorVm
                        {
                            Id = wd.Id,
                            WarehouseId = wd.WarehouseId,
                            DoorNo = wd.DoorNo,
                            WarehouseName = w.Name
                        };
            var data = await query.OrderBy(c => c.Id).ToListAsync();
            return Ok(data);
        }
    }
}