using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.BackendServer.Controllers
{
    public class DeliveryOrderController : BaseController<DeliveryOrder>
    {
        public DeliveryOrderController(WhContext context, IConfiguration configuration) : base(context, configuration)
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