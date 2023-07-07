using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.BackendServer.Controllers
{
    public class CurrentPositionsController : BaseController<CurrentPosition>
    {
        public CurrentPositionsController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            CurrentPosition res = await _context.CurrentPositions.FindAsync(id);
            if (res != null)
            {
                res.Status = CurrentPositionStatus.Deleted;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }
    }
}