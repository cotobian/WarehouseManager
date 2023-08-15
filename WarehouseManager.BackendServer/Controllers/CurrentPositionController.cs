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

        [HttpGet("CurrentStock/{warehouseid}")]
        public async Task<IActionResult> GetCurrentStock(int warehouseid)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var sql = @"select wp.Bay,wp.Row,wp.Tier,rd.PO,rd.Item,pd.Quantity
                from CurrentPositions cp join WarehousePositions wp
                on cp.PositionId=wp.Id join PalletDetails pd on pd.Id=cp.PalletId
                join ReceiptDetails rd on rd.Id=pd.ReceiptDetailId where
                wp.WarehouseId=@WarehouseId and cp.Status=@Status";
                var parameters = new { WarehouseId = warehouseid, Status = CurrentPositionStatus.Occupied };
                var result = await conn.QueryAsync<CurrentStockVm>(sql, parameters, null, 120, CommandType.Text);
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
                StackLayoutVm vm = new StackLayoutVm();
                List<RowDisplay> rowList = new List<RowDisplay>();
                List<string> rows = await _context.WarehousePositions
                    .Where(c => c.Bay.Equals(bay) && c.Status == true)
                    .Select(c => c.Row)
                    .Distinct()
                    .ToListAsync();
                foreach (string row in rows)
                {
                    RowDisplay rowDisplay = new RowDisplay();
                    rowDisplay.RowText = row;
                    List<string> colorList = new List<string>();
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
                    if (colorList.Count > 1) rowDisplay.RowColor = "#FF6347";
                    else if (colorList.Count == 1) rowDisplay.RowColor = colorList[0];
                    else rowDisplay.RowColor = "";
                    rowList.Add(rowDisplay);
                }
                vm.Bay = bay;
                vm.RowList = rowList;
                result.Add(vm);
            }
            return Ok(result);
        }

        [HttpGet("DisplayTier/{warehouseid}/Bay/{bay}/Row/{row}")]
        public async Task<IActionResult> DisplayTier(string bay, string row, int warehouseid)
        {
            DisplayTierVm tierVm = new DisplayTierVm();
            WarehousePosition warehousePosition = await _context.WarehousePositions
                .Where(c => c.Bay.Equals(bay) && c.Row.Equals(row) && c.WarehouseId == warehouseid && c.Status == true)
                .FirstOrDefaultAsync();
            if (warehousePosition == null)
            {
                return BadRequest("Warehouse Position not found!");
            }
            CurrentPosition currentPosition = await _context.CurrentPositions
                .Where(c => c.PositionId == warehousePosition.Id && c.Status != CurrentPositionStatus.Deleted)
                .FirstOrDefaultAsync();
            if (currentPosition == null)
            {
                return BadRequest("Stacked Position not found!");
            }
            if (currentPosition.PalletId != null)
            {
                List<PalletDetail> pallet = await _context.PalletDetails
                    .Where(c => c.PalletId == currentPosition.PalletId)
                    .ToListAsync();
                foreach (PalletDetail palletDetail in pallet)
                {
                    ReceiptDetail receiptDetail = await _context.ReceiptDetails
                        .Where(c => c.Id == palletDetail.ReceiptDetailId)
                        .FirstOrDefaultAsync();
                    string item = receiptDetail.PO + "-" + (string.IsNullOrEmpty(receiptDetail.Item) ? "" : receiptDetail.Item) + "-" + receiptDetail.ReceivedQuantity;
                    tierVm.ListItem.Add(item);
                }
            }
            tierVm.Tier = warehousePosition.Tier;
            return Ok(tierVm);
        }
    }
}