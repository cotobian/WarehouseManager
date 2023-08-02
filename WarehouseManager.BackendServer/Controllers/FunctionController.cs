using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;

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

        [HttpGet("User/{userid}")]
        public async Task<IActionResult> GetFunctionByUser(int userid)
        {
            try
            {
                var query = from up in _context.UserPermissions
                            join f in _context.Functions on up.FunctionId equals f.Id
                            where up.Command == Command.READ & up.UserId == userid & up.Status == true
                            orderby f.SortOrder
                            select f;
                List<Function> functions = await query.ToListAsync();
                return Ok(functions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}