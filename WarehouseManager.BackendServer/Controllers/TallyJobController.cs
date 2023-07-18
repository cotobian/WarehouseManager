using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.BackendServer.Controllers
{
    public class TallyJobController : BaseController<TallyJob>
    {
        public TallyJobController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        [HttpPost("ReceiptOrder")]
        public async Task<IActionResult> CreateByReceiptOrder([FromBody] int orderId)
        {
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