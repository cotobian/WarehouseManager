using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.BackendServer.Data.Validators;

namespace WarehouseManager.BackendServer.Controllers
{
    public class PalletController : BaseController<Pallet>
    {
        public PalletController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        [HttpPost("CreateList")]
        public async Task<IActionResult> CreateList(int palletNo)
        {
            int count = _context.Pallets.Count(c => c.CreatedDate.Date == DateTime.Now);
            for (int i = 1; i <= palletNo; i++)
            {
                Pallet pallet = new Pallet();
                pallet.PalletNo = DateTime.Now.ToString("ddMMyy") + "_" + (count + i).ToString("D4");
                _context.Pallets.Add(pallet);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}