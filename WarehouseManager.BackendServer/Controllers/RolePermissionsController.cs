using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Admin.RolePermission;

namespace WarehouseManager.BackendServer.Controllers
{
    public class RolePermissionsController : BaseController<RolePermission>
    {
        public RolePermissionsController(WhContext context) : base(context)
        {
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetById(int id)
        {
            RolePermission rolePermission = await _context.RolePermissions.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (rolePermission == null) return NotFound();
            var data = from rp in _context.RolePermissions
                       join r in _context.Roles on rp.RoleId equals r.Id
                       join
        }
    }
}