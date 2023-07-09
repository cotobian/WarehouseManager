using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Admin.User;
using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.BackendServer.Controllers
{
    public class UserController : BaseController<User>
    {
        public UserController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        #region Public Methods

        [HttpGet]
        public override async Task<IActionResult> GetAll()
        {
            var query = from u in _context.Users
                        join d in _context.Departments on u.DepartmentId equals d.Id
                        join r in _context.Roles on u.RoleId equals r.Id
                        where u.Status == UserStatus.Working
                        select new GetUserVm
                        {
                            Id = u.Id,
                            FullName = u.FullName,
                            Username = u.Username,
                            RoleName = r.Name,
                            DepartmentName = d.Name,
                            RoleId = r.Id,
                            DepartmentId = d.Id
                        };
            var data = await query.OrderBy(c => c.Id).ToListAsync();
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            User res = await _context.Users.FindAsync(id);
            if (res != null)
            {
                res.Status = UserStatus.Delete;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }

        #endregion Public Methods
    }
}