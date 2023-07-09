using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class FunctionController : BaseController<Function>
    {
        public FunctionController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        [HttpGet("Parent")]
        public async Task<IActionResult> GetParents()
        {
            return Ok(await _context.Functions.Where(c => c.Status == true && c.ParentId == null).ToListAsync());
        }
    }
}