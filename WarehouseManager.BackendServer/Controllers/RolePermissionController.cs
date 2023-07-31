﻿using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Admin.RolePermission;
using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.BackendServer.Controllers
{
    public class RolePermissionController : BaseController<RolePermission>
    {
        public RolePermissionController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        [HttpGet("Role/{id}")]
        public async Task<IActionResult> GetByRoleId(int id)
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
                ISNULL(SUM(CASE WHEN rp.Command = 1 THEN 1 ELSE 0 END), 0) AS hasCreate,
                ISNULL(SUM(CASE WHEN rp.Command = 2 THEN 1 ELSE 0 END), 0) AS hasView,
                ISNULL(SUM(CASE WHEN rp.Command = 3 THEN 1 ELSE 0 END), 0) AS hasUpdate,
                ISNULL(SUM(CASE WHEN rp.Command = 4 THEN 1 ELSE 0 END), 0) AS hasDelete,
                ISNULL(SUM(CASE WHEN rp.Command = 5 THEN 1 ELSE 0 END), 0) AS hasApprove
                FROM Functions f
                LEFT JOIN RolePermissions rp ON f.Id = rp.FunctionId AND rp.RoleId = @RoleId
                WHERE f.Status = 1
                GROUP BY f.Id, f.Name, f.ParentId, f.SortOrder
                ORDER BY f.SortOrder";
                var parameters = new { RoleId = id };
                var result = await conn.QueryAsync<GetRolePermissionVm>(sql, parameters, null, 120, CommandType.Text);
                return Ok(result.ToList());
            }
        }

        [HttpPost("Role")]
        public async Task<IActionResult> CreatePermissionForRole(List<CreateRolePermissionVm> list)
        {
            foreach (CreateRolePermissionVm item in list)
            {
                RolePermission role = new RolePermission();
                role.RoleId = item.RoleId;
                role.FunctionId = item.FunctionId;
                Command cmd;
                if (Enum.TryParse(item.Command, out cmd))
                {
                    role.Command = cmd;
                }
                else
                {
                    return BadRequest("Cannot find command: " + item.Command);
                }
                role.Status = true;
                _context.RolePermissions.Add(role);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}