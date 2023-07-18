using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Warehouse.DeliveryDetail;

namespace WarehouseManager.BackendServer.Controllers
{
    public class DeliveryDetailController : BaseController<DeliveryDetail>
    {
        public DeliveryDetailController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        [HttpGet("orderid/{id}")]
        public virtual async Task<IActionResult> GetByOrderId(int orderid)
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
                var result = await conn.QueryAsync<GetDeliveryDetailVm>(sql, parameters, null, 120, CommandType.Text);
                return Ok(result.ToList());
            }
        }
    }
}