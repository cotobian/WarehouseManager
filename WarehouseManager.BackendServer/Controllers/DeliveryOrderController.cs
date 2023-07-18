using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.BackendServer.Data.Validators;
using WarehouseManager.ViewModels.Constants;
using WarehouseManager.ViewModels.Warehouse.DeliveryOrder;

namespace WarehouseManager.BackendServer.Controllers
{
    public class DeliveryOrderController : BaseController<DeliveryOrder>
    {
        public DeliveryOrderController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        [HttpGet]
        public override async Task<IActionResult> GetAll()
        {
            if (GetUserId() != 0)
            {
                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        await conn.OpenAsync();
                    }
                    var sql = @"select do.*,a.Name as DoorName from
                    DeliveryOrders do left join WarehouseDoors wh on do.WarehouseDoorId=wh.Id
                    where do.OrderStatus <> @OrderStatus and do.CreatedUserId=@CreatedUserId";
                    var parameters = new { OrderStatus = OrderStatus.Deleted, CreatedUserID = GetUserId() };
                    var result = await conn.QueryAsync<GetDeliveryOrderVm>(sql, parameters, null, 120, CommandType.Text);
                    return Ok(result.ToList());
                }
            }
            else return BadRequest("User not logged in!");
        }

        [HttpPost]
        public override async Task<IActionResult> Post(DeliveryOrder item)
        {
            int count = _context.DeliveryOrders.Count(c => c.CreatedDate.Date == DateTime.Now) + 1;
            item.OrderNo = "SO/" + DateTime.Now.ToString("ddMMyy") + "/" + count.ToString("D4");
            item.CreatedDate = DateTime.Now;
            item.CreatedUserId = GetUserId();
            DeliveryOrderValidator validator = new DeliveryOrderValidator();
            var validateResult = validator.Validate(item);
            if (validateResult.IsValid)
            {
                _context.DeliveryOrders.Add(item);
                await _context.SaveChangesAsync();
                return Ok(item);
            }
            else
            {
                return BadRequest($"Property '{validateResult.Errors[0].PropertyName}': {validateResult.Errors[0].ErrorMessage}");
            }
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            DeliveryOrder res = await _context.DeliveryOrders.FindAsync(id);
            if (res != null)
            {
                res.OrderStatus = OrderStatus.Deleted;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }
    }
}