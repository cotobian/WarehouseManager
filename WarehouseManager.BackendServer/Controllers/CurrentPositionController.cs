using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            List<StackLayoutVm> result = new List<StackLayoutVm>();
            List<string> bayList = await _context.WarehousePositions
                .Where(c => c.WarehouseId == warehouseid && c.Status == true)
                .Select(c => c.Bay)
                .Distinct()
                .ToListAsync();
            foreach (string bay in bayList)
            {
                List<string> colorList = new List<string>();
                StackLayoutVm vm = new StackLayoutVm();
                vm.Bay = bay;
                vm.RowList = await _context.WarehousePositions
                    .Where(c => c.Bay.Equals(bay) && c.Status == true)
                    .Select(c => c.Row)
                    .Distinct()
                    .ToListAsync();
                foreach (string row in vm.RowList)
                {
                    List<int> posList = await _context.WarehousePositions
                        .Where(c => c.Bay.Equals(bay) && c.Row.Equals(row) && c.Status == true)
                        .Select(c => c.Id)
                        .ToListAsync();
                    foreach (int pos in posList)
                    {
                        string color = await _context.CurrentPositions
                            .Where(c => c.PositionId == pos && c.Status != CurrentPositionStatus.Deleted)
                            .Select(c => c.Color)
                            .FirstOrDefaultAsync();
                        if (!string.IsNullOrEmpty(color) && !colorList.Contains(color))
                        {
                            colorList.Add(color);
                        }
                    }
                }
                if (colorList.Count > 1) vm.Color = "#f58a42";
                else if (colorList.Count == 1) vm.Color = colorList[0];
                else vm.Color = string.Empty;
                result.Add(vm);
            }
            return Ok(result);
        }
    }
}