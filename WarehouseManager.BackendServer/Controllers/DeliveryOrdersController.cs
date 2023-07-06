using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.BackendServer.Controllers
{
    public class DeliveryOrdersController : BaseController<DeliveryOrder>
    {
        public DeliveryOrdersController(WhContext context) : base(context)
        {
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            DeliveryOrder res = await _context.DeliveryOrders.FindAsync(id);
            if (res != null)
            {
                res.OrderStatus = OrderStatus.Deleted;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }
    }
}