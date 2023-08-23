using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;
using WarehouseManager.ViewModels.Warehouse.Pallet;

namespace WarehouseManager.BackendServer.Controllers
{
    public class PalletController : BaseController<Pallet>
    {
        public PalletController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        [HttpGet("CreateList/{palletNo}")]
        public async Task<IActionResult> CreateList(int palletNo)
        {
            int count = _context.Pallets.Count(c => c.CreatedDate.Date == DateTime.Now.Date && c.Status != PalletStatus.Deleted);
            for (int i = 1; i <= palletNo; i++)
            {
                Pallet pallet = new Pallet();
                pallet.Status = PalletStatus.Created;
                pallet.PalletNo = "PL_" + DateTime.Now.ToString("ddMMyy") + "_" + (count + i).ToString("D4");
                _context.Pallets.Add(pallet);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("PrintPallet/{palletIds}")]
        public async Task<IActionResult> PrintPallet(string palletIds)
        {
            List<int> palletNo = palletIds.Split(',').Select(int.Parse).ToList();
            string result = "";
            foreach (int palletno in palletNo)
            {
                Pallet pallet = await _context.Pallets.Where(c => c.Id == palletno && c.Status != PalletStatus.Deleted).FirstOrDefaultAsync();
                if (pallet != null)
                {
                    pallet.Status = PalletStatus.Printed;
                    result += pallet.PalletNo + ",";
                }
            }
            if (result.EndsWith(","))
            {
                result = result.Remove(result.Length - 1);
            }

            await _context.SaveChangesAsync();
            return Ok(result);
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
                    var sql = @"select *,case when Status=@Status1 then N'Khởi tạo' else N'Đã in' end as StatusText,
                    convert(varchar,CreatedDate,103) as CreatedDateText from Pallets where Status!=@Status2 order by Id desc";
                    var parameters = new { Status1 = PalletStatus.Created, Status2 = PalletStatus.Deleted };
                    var result = await conn.QueryAsync<GetPalletVm>(sql, parameters, null, 120, CommandType.Text);
                    return Ok(result.ToList());
                }
            }
            else return BadRequest("User not logged in!");
        }
    }
}