using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;
using WarehouseManager.ViewModels.Warehouse.CurrentPosition;

namespace WarehouseManager.BackendServer.Controllers
{
    public class CurrentPositionController : BaseController<CurrentPosition>
    {
        public CurrentPositionController(WhContext context, IConfiguration configuration) : base(context, configuration)
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

        [HttpGet("PositionLayout/{warehouseid}")]
        public async Task<IActionResult> GetPositionLayout(int warehouseid)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var sql = @"select distinct cp.PalletId,wp.Bay,wp.Row from
                CurrentPositions cp join WarehousePositions wp
                on cp.PositionId=wp.Id where wp.WarehouseId=@WarehouseId and cp.Status!=@Status";
                var parameters = new { WarehouseId = warehouseid, Status = CurrentPositionStatus.Deleted };
                var result = await conn.QueryAsync<LayoutPositionVm>(sql, parameters, null, 120, CommandType.Text);
                return Ok(result.ToList());
            }
        }

        [HttpGet("StackLayout/{warehouseid}")]
        public async Task<IActionResult> GetStackLayout(int warehouseid)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var sql = @"select * from CurrentPositions cp left join PalletDetails pd
                on cp.PalletId=pd.PalletId left join WarehousePositions wp
                on cp.PositionId=wp.Id where cp.Status=@Status and wp.WarehouseId=@WarehouseId";
                var parameters = new { WarehouseId = warehouseid, Status = CurrentPositionStatus.Occupied };
                var result = await conn.QueryAsync<CurrentStockVm>(sql, parameters, null, 120, CommandType.Text);
                return Ok(result.ToList());
            }
        }
    }
}