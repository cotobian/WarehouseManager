using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.BackendServer.Data.Validators;
using WarehouseManager.ViewModels.Constants;
using WarehouseManager.ViewModels.Warehouse.ReceiptOrder;

namespace WarehouseManager.BackendServer.Controllers
{
    public class ReceiptOrderController : BaseController<ReceiptOrder>
    {
        public ReceiptOrderController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        private int GetUserId()
        {
            var nameIdentifierClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (nameIdentifierClaim != null)
            {
                int userId = int.Parse(nameIdentifierClaim.Value);
                return userId;
            }
            else return 0;
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
                    var sql = @"select ro.*,a.Name as AgentName, c.Name as CustomerName from
                    ReceiptOrders ro left join Agents a on ro.AgentId=a.Id
                    left join Customers c on ro.CustomerId=c.Id
                    where ro.OrderStatus <> @OrderStatus and ro.CreatedUserId=@CreatedUserId";
                    var parameters = new { OrderStatus = OrderStatus.Deleted, CreatedUserID = GetUserId() };
                    var result = await conn.QueryAsync<GetReceiptOrderVm>(sql, parameters, null, 120, CommandType.Text);
                    return Ok(result.ToList());
                }
            }
            else return BadRequest("User not logged in!");
        }

        [HttpPost]
        public override async Task<IActionResult> Post(ReceiptOrder item)
        {
            int count = _context.ReceiptOrders.Count(c => c.CreatedDate.Date == DateTime.Now) + 1;
            item.OrderNo = "GRN/" + DateTime.Now.ToString("ddMMyy") + "/" + count.ToString("D4");
            item.CreatedDate = DateTime.Now;
            item.CreatedUserId = GetUserId();
            ReceiptOrderValidator validator = new ReceiptOrderValidator();
            var validateResult = validator.Validate(item);
            if (validateResult.IsValid)
            {
                _context.ReceiptOrders.Add(item);
                await _context.SaveChangesAsync();
                return Ok(item);
            }
            else
            {
                return BadRequest($"Property '{validateResult.Errors[0].PropertyName}': {validateResult.Errors[0].ErrorMessage}");
            }
        }

        [HttpDelete]
        public override async Task<IActionResult> Delete(int id)
        {
            ReceiptOrder res = await _context.ReceiptOrders.FindAsync(id);
            if (res != null)
            {
                res.OrderStatus = OrderStatus.Deleted;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else return BadRequest("No record found");
        }
    }
}