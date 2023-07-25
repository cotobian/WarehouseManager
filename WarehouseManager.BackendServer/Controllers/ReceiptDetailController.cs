using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

        [HttpGet("orderid/{id}")]
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

        [HttpGet("PO/{po}/Item/{item}")]
        public async Task<IActionResult> GetRemainQuantity(string po, string item)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var sql = @"select top 1 (rd.ExpectedQuantity - rd.ReceivedQuantity) as Quantity from ReceiptDetails rd
                join ReceiptOrders ro on rd.OrderId = ro.Id
                where ro.OrderStatus in (@Status1,@Status2) and rd.PO = @PO and rd.Item = @Item";
                var parameters = new { Status1 = OrderStatus.Created, Status2 = OrderStatus.Processing, PO = po, Item = item };
                int result = await conn.QuerySingleAsync<int>(sql, parameters, null, 120, CommandType.Text);
                return Ok(result);
            }
        }
    }
}