using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Admin.UserPermission;

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
                ISNULL(SUM(CASE WHEN up.Command = 1 THEN 1 ELSE 0 END), 0) AS hasCreate,
                ISNULL(SUM(CASE WHEN up.Command = 2 THEN 1 ELSE 0 END), 0) AS hasView,
                ISNULL(SUM(CASE WHEN up.Command = 3 THEN 1 ELSE 0 END), 0) AS hasUpdate,
                ISNULL(SUM(CASE WHEN up.Command = 4 THEN 1 ELSE 0 END), 0) AS hasDelete,
                ISNULL(SUM(CASE WHEN up.Command = 5 THEN 1 ELSE 0 END), 0) AS hasApprove
                FROM Functions f
                LEFT JOIN UserPermissions up ON f.Id = up.FunctionId AND up.UserId = @UserId
                WHERE f.Status = 1
                GROUP BY f.Id, f.Name, f.ParentId,f.SortOrder
                ORDER BY f.SortOrder";
                var parameters = new { UserId = id };
                var result = await conn.QueryAsync<UserPermissionVm>(sql, parameters, null, 120, CommandType.Text);
                return Ok(result.ToList());
            }
        }
    }
}