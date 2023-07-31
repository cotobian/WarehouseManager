using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Admin.UserPermission;
using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.BackendServer.Controllers
{
    public class UserPermissionController : BaseController<UserPermission>
    {
        public UserPermissionController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        [HttpGet("User/{id}")]
        public async Task<IActionResult> GetByUserId(int id)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var sql = @"SELECT f.Id,
                f.Name as FunctionName,
                ISNULL(f.ParentId,0) as ParentId,
                ISNULL(SUM(CASE WHEN up.Command = @Create THEN 1 ELSE 0 END), 0) AS hasCreate,
                ISNULL(SUM(CASE WHEN up.Command = @Read THEN 1 ELSE 0 END), 0) AS hasView,
                ISNULL(SUM(CASE WHEN up.Command = @Update THEN 1 ELSE 0 END), 0) AS hasUpdate,
                ISNULL(SUM(CASE WHEN up.Command = @Delete THEN 1 ELSE 0 END), 0) AS hasDelete,
                ISNULL(SUM(CASE WHEN up.Command = @Approve THEN 1 ELSE 0 END), 0) AS hasApprove
                FROM Functions f
                LEFT JOIN UserPermissions up ON f.Id = up.FunctionId AND up.UserId = @UserId
                WHERE f.Status = 1
                GROUP BY f.Id, f.Name, f.ParentId,f.SortOrder
                ORDER BY f.SortOrder";
                var parameters = new { UserId = id, Create = Command.CREATE, Read = Command.READ, Update = Command.UPDATE, Delete = Command.DELETE, Approve = Command.APPROVE };
                var result = await conn.QueryAsync<GetUserPermissionVm>(sql, parameters, null, 120, CommandType.Text);
                return Ok(result.ToList());
            }
        }

        [HttpPost("User")]
        public async Task<IActionResult> CreatePermissionForUser(List<CreateUserPermissionVm> list)
        {
            var deletePermissions = await _context.UserPermissions.Where(x => x.UserId == list[0].UserId).ToListAsync();
            _context.UserPermissions.RemoveRange(deletePermissions);
            foreach (CreateUserPermissionVm item in list)
            {
                UserPermission user = new UserPermission();
                user.UserId = item.UserId;
                user.FunctionId = item.FunctionId;
                Command cmd;
                if (Enum.TryParse(item.Command, out cmd))
                {
                    user.Command = cmd;
                }
                else
                {
                    return BadRequest("Cannot find command: " + item.Command);
                }
                user.Status = true;
                _context.UserPermissions.Add(user);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}