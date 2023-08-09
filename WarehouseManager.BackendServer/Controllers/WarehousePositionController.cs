using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.BackendServer.Data.Validators;
using WarehouseManager.ViewModels.Index;

namespace WarehouseManager.BackendServer.Controllers
{
    public class WarehousePositionController : BaseController<WarehousePosition>
    {
        public WarehousePositionController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        [HttpGet]
        public override async Task<IActionResult> GetAll()
        {
            var query = from wp in _context.WarehousePositions
                        join w in _context.Warehouses on wp.WarehouseId equals w.Id
                        where wp.Status == true && w.Status == true
                        select new GetWarehousePositionVm
                        {
                            Id = wp.Id,
                            WarehouseId = wp.WarehouseId,
                            Bay = wp.Bay,
                            Row = wp.Row,
                            Tier = wp.Tier,
                            WarehouseName = w.Name
                        };
            var data = await query.OrderBy(c => c.Id).ToListAsync();
            return Ok(data);
        }

        [HttpPost]
        public override async Task<IActionResult> Post(WarehousePosition item)
        {
            WarehousePositionValidator validator = new WarehousePositionValidator();
            var results = validator.Validate(item);

            if (results.IsValid)
            {
                _context.WarehousePositions.Add(item);
                await _context.SaveChangesAsync();
                CurrentPosition currentPosition = new CurrentPosition();
                currentPosition.PositionId = item.Id;
                currentPosition.Status = ViewModels.Constants.CurrentPositionStatus.Empty;
                _context.CurrentPositions.Add(currentPosition);
                await _context.SaveChangesAsync();
                return Ok(item);
            }
            else return BadRequest($"Property '{results.Errors[0].PropertyName}': {results.Errors[0].ErrorMessage}");
        }
    }
}