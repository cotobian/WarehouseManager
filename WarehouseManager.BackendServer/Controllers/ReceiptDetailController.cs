using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;
using WarehouseManager.ViewModels.Warehouse.ReceiptDetail;

namespace WarehouseManager.BackendServer.Controllers
{
    public class ReceiptDetailController : BaseController<ReceiptDetail>
    {
        public ReceiptDetailController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        [HttpGet("orderid/{orderid}")]
        public async Task<IActionResult> GetByOrderId(int orderid)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var sql = @"select rd.*,u.Name as UnitName from ReceiptDetails rd left join Units u
                on rd.UnitId=u.Id where rd.OrderId=@OrderId and rd.Status=1";
                var parameters = new { OrderId = orderid };
                var result = await conn.QueryAsync<GetReceiptDetailVm>(sql, parameters, null, 120, CommandType.Text);
                return Ok(result.ToList());
            }
        }

        [HttpGet("availablePO")]
        public async Task<IActionResult> GetAllAvailablePO()
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var sql = @"select rd.PO as PO from ReceiptDetails rd join ReceiptOrders ro on rd.OrderId = ro.Id
                where ro.OrderStatus in (@Status1,@Status2)";
                var parameters = new { Status1 = OrderStatus.Created, Status2 = OrderStatus.Processing };
                var result = await conn.QueryAsync<string>(sql, parameters, null, 120, CommandType.Text);
                return Ok(result.ToList());
            }
        }

        [HttpGet("availableItem")]
        public async Task<IActionResult> GetAllAvailableItem()
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var sql = @"select distinct(rd.Item) from ReceiptDetails rd join ReceiptOrders ro on rd.OrderId = ro.Id
                where ro.OrderStatus in (@Status1,@Status2)";
                var parameters = new { Status1 = OrderStatus.Created, Status2 = OrderStatus.Processing };
                var result = await conn.QueryAsync<string>(sql, parameters, null, 120, CommandType.Text);
                return Ok(result.ToList());
            }
        }

        [HttpPost("GetRemain")]
        public async Task<IActionResult> GetRemainQuantity(GetRemainVm remain)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                int result;
                if (!string.IsNullOrEmpty(remain.Item))
                {
                    var sql = @"select top 1 (rd.ExpectedQuantity - rd.ReceivedQuantity) as Quantity from ReceiptDetails rd
                    join ReceiptOrders ro on rd.OrderId = ro.Id
                    where ro.OrderStatus in (@Status1,@Status2) and rd.PO = @PO and rd.Item = @Item";
                    var parameters = new { Status1 = OrderStatus.Created, Status2 = OrderStatus.Processing, PO = remain.PO, Item = remain.Item };
                    result = await conn.QuerySingleAsync<int>(sql, parameters, null, 120, CommandType.Text);
                }
                else
                {
                    var sql = @"select top 1 (rd.ExpectedQuantity - rd.ReceivedQuantity) as Quantity from ReceiptDetails rd
                    join ReceiptOrders ro on rd.OrderId = ro.Id
                    where ro.OrderStatus in (@Status1,@Status2) and rd.PO = @PO";
                    var parameters = new { Status1 = OrderStatus.Created, Status2 = OrderStatus.Processing, PO = remain.PO };
                    result = await conn.QuerySingleAsync<int>(sql, parameters, null, 120, CommandType.Text);
                }
                return Ok(result);
            }
        }

        [HttpPost("UploadExcel")]
        public async Task<IActionResult> UploadExcel([FromBody] List<List<string>> uploadedData)
        {
            foreach (List<string> item in uploadedData)
            {
                ReceiptDetail receiptDetail = new ReceiptDetail();
                receiptDetail.PO = item[0];
                receiptDetail.Item = item[1];
                receiptDetail.ExpectedQuantity = int.Parse(item[2]);
                if (!string.IsNullOrEmpty(item[3]))
                {
                    Unit unit = await _context.Units.Where(c => c.Name.Equals(item[3])).FirstOrDefaultAsync();
                    if (unit == null) return BadRequest("Wrong Unit Input!");
                    receiptDetail.UnitId = unit.Id;
                }
                receiptDetail.Weight = string.IsNullOrEmpty(item[4]) ? 0 : decimal.Parse(item[4]);
                receiptDetail.CBM = string.IsNullOrEmpty(item[5]) ? 0 : decimal.Parse(item[5]);
                receiptDetail.Status = true;
                receiptDetail.CustomDeclareNo = string.IsNullOrEmpty(item[6]) ? "" : item[6];
                receiptDetail.Size = string.IsNullOrEmpty(item[7]) ? "" : item[7];
                receiptDetail.OrderId = int.Parse(item[8]);
                _context.ReceiptDetails.Add(receiptDetail);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}