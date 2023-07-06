using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.BackendServer.Controllers
{
    public class ForkliftJobsController : BaseController<ForkliftJob>
    {
        public ForkliftJobsController(WhContext context) : base(context)
        {
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            ForkliftJob res = await _context.ForkliftJobs.FindAsync(id);
            if (res != null)
            {
                res.JobStatus = ForkLiftJobStatus.Deleted;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }
    }
}