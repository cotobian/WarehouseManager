using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;
using WarehouseManager.ViewModels.Warehouse.ForkliftJob;

namespace WarehouseManager.BackendServer.Controllers
{
    public class ForkliftJobController : BaseController<ForkliftJob>
    {
        public ForkliftJobController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        [HttpGet]
        public override async Task<IActionResult> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var sql = @"select f.*,p.PalletNo,case when f.PositionId is not null then CONCAT(wp.Bay,'.',wp.Row,'.',wp.Tier) else '' end as PositionName,CONVERT(varchar,f.CreatedDate,103) as CreatedDateText,
                ISNULL(f.CompletedDate,CONVERT(varchar,f.CompletedDate,103)) as CompletedDateText,u1.FullName as CreatedUserName,u2.FullName as CompletedUserName,
                case when f.JobStatus = 0 then N'Khởi tạo' when f.JobStatus = 1 then N'Xử lý' when f.JobStatus=2 then N'Hoàn tất'
                else N'Trouble' end as JobStatusText,case when f.jobType = 0 then N'Nhập kho' when f.jobType = 1 then N'Xuất kho'
                when f.JobStatus=2 then N'Đảo chuyển' end as jobTypeText from ForkliftJobs f join Pallets p on f.PalledId=p.Id
                left join WarehousePositions wp on wp.Id=f.PositionId
                join Users u1 on u1.Id=f.CreatedUserId left join Users u2
                on u2.Id = f.CompletedUserId where f.JobStatus <> @JobStatus order by f.Id desc";
                var parameters = new { JobStatus = JobStatus.Deleted };
                var result = await conn.QueryAsync<GetForkliftJobVm>(sql, parameters, null, 120, CommandType.Text);
                return Ok(result.ToList());
            }
        }

        [HttpPost("CompleteJob")]
        public async Task<IActionResult> CompleteJob([FromBody] CompleteForkliftJobVm vm)
        {
            Pallet pallet = await _context.Pallets.Where(c => c.PalletNo == vm.PalletNo && c.Status == PalletStatus.Printed).FirstOrDefaultAsync();
            if (pallet == null) return BadRequest("No Pallet found");
            ForkliftJob job = await _context.ForkliftJobs.Where(c => c.PalledId == pallet.Id &&
            c.JobStatus != JobStatus.Deleted && c.JobStatus != JobStatus.Completed).FirstOrDefaultAsync();
            if (job == null) return BadRequest("No Job found");
            string[] pos = vm.PositionName.Split('.');
            if (pos.Length != 3) return BadRequest("Wrong Position!");
            WarehousePosition position = await _context.WarehousePositions.Where(c => c.Bay == pos[0] && c.Row == pos[1]
            && c.Row == pos[2] && c.Status == true).FirstOrDefaultAsync();
            if (position == null) return BadRequest("No Position found");
            PalletDetail palletDetail = await _context.PalletDetails.Where(c => c.PalletId == pallet.Id).FirstOrDefaultAsync();
            ReceiptDetail receiptDetail = await _context.ReceiptDetails.Where(c => c.Id == palletDetail.ReceiptDetailId).FirstOrDefaultAsync();
            ReceiptOrder receiptOrder = await _context.ReceiptOrders.Where(c => c.Id == receiptDetail.OrderId).FirstOrDefaultAsync();
            Customer customer = await _context.Customers.Where(c => c.Id == receiptOrder.CustomerId).FirstOrDefaultAsync();

            job.PositionId = position.Id;
            job.CompletedDate = DateTime.Now;
            job.CompletedUserId = GetUserId();
            job.JobStatus = JobStatus.Completed;

            CurrentPosition cp = _context.CurrentPositions.Where(c => c.PositionId == position.Id && c.Status != CurrentPositionStatus.Deleted).FirstOrDefault();
            cp.PalletId = pallet.Id;
            cp.Status = CurrentPositionStatus.Occupied;
            cp.Color = customer.Color;

            await _context.SaveChangesAsync();
            return Ok(job.Id);
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            ForkliftJob res = await _context.ForkliftJobs.FindAsync(id);
            if (res != null)
            {
                res.JobStatus = JobStatus.Deleted;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }
    }
}