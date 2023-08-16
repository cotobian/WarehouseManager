using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;
using WarehouseManager.ViewModels.Warehouse.TallyJob;

namespace WarehouseManager.BackendServer.Controllers
{
    public class TallyJobController : BaseController<TallyJob>
    {
        public TallyJobController(WhContext context, IConfiguration configuration) : base(context, configuration)
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
                var sql = @"select f.*,p.PalletNo,CONVERT(varchar,f.CreatedDate,103) as CreatedDateText,
                ISNULL(f.CompletedDate,CONVERT(varchar,f.CompletedDate,103)) as CompletedDateText,u1.FullName as CreatedUserName,u2.FullName as CompletedUserName,
                case when f.JobStatus = 0 then N'Khởi tạo' when f.JobStatus = 1 then N'Xử lý' when f.JobStatus=2 then N'Hoàn tất'
                else N'Trouble' end as JobStatusText,case when f.jobType = 0 then N'Nhập kho' when f.jobType = 1 then N'Xuất kho'
                when f.JobStatus=2 then N'Đảo chuyển' end as jobTypeText from TallyJobs f join Pallets p on f.PalletId=p.Id
                join Users u1 on u1.Id=f.CreatedUserId left join Users u2
                on u2.Id = f.CompletedUserId where f.JobStatus!=@JobStatus order by f.Id desc";
                var parameters = new { JobStatus = JobStatus.Deleted };
                var result = await conn.QueryAsync<GetTallyJobVm>(sql, parameters, null, 120, CommandType.Text);
                return Ok(result.ToList());
            }
        }

        [HttpPost("ReceiptOrder")]
        public async Task<IActionResult> CreateByReceiptOrder([FromBody] int orderId)
        {
            ReceiptOrder receiptOrder = _context.ReceiptOrders.Where(c => c.Id == orderId).FirstOrDefault();
            receiptOrder.OrderStatus = OrderStatus.Processing;
            List<ReceiptDetail> receiptDetailList = await _context.ReceiptDetails.Where(c => c.OrderId == orderId && c.Status == true).ToListAsync();
            if (receiptDetailList.Count > 0)
            {
                foreach (ReceiptDetail detail in receiptDetailList)
                {
                    TallyJob job = new TallyJob();
                    job.ReceiptDetailId = detail.Id;
                    job.Quantity = detail.ExpectedQuantity;
                    job.CreatedDate = DateTime.Now;
                    job.CreatedUserId = GetUserId();
                    job.jobStatus = JobStatus.Created;
                    job.jobType = JobType.Inbound;
                    _context.TallyJobs.Add(job);
                }
                await _context.SaveChangesAsync();
                return Ok();
            }
            else return BadRequest("No available detail of Receipt Order " + orderId + "!");
        }

        [HttpPost("ReceiptDetail")]
        public async Task<IActionResult> CreateByReceiptDetail([FromBody] int detailId)
        {
            ReceiptDetail receiptDetail = await _context.ReceiptDetails.Where(c => c.Id == detailId && c.Status == true).FirstOrDefaultAsync();
            if (receiptDetail != null)
            {
                TallyJob job = new TallyJob();
                job.ReceiptDetailId = detailId;
                job.Quantity = receiptDetail.ExpectedQuantity;
                job.CreatedDate = DateTime.Now;
                job.CreatedUserId = GetUserId();
                job.jobStatus = JobStatus.Created;
                job.jobType = JobType.Inbound;
                _context.TallyJobs.Add(job);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else return BadRequest("No Receipt Detail of id " + detailId + "!");
        }
    }
}