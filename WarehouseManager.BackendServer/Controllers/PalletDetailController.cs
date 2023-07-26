using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;
using WarehouseManager.ViewModels.Warehouse.PalletDetail;

namespace WarehouseManager.BackendServer.Controllers
{
    public class PalletDetailController : BaseController<PalletDetail>
    {
        public PalletDetailController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        [HttpPost("PostListVm")]
        public async Task<IActionResult> PostListVm([FromBody] List<CreatePalletDetailVm> palletList)
        {
            Pallet pallet = await _context.Pallets.Where(c => c.PalletNo == palletList[0].PalletNo && c.Status == true).FirstOrDefaultAsync();
            if (pallet == null) return BadRequest("Cannot find Pallet Number: " + palletList[0].PalletNo);
            foreach (CreatePalletDetailVm p in palletList)
            {
                ReceiptDetail detail = (from rd in _context.ReceiptDetails
                                        join ro in _context.ReceiptOrders
                                        on rd.OrderId equals ro.Id
                                        where rd.PO == p.PO && rd.Item == p.Item
                                        && (ro.OrderStatus == OrderStatus.Created || ro.OrderStatus == OrderStatus.Processing)
                                        select rd).FirstOrDefault();
                if (detail == null) return BadRequest("Cannot find PO: " + p.PO +
                    (string.IsNullOrEmpty(p.Item) ? "" : "Item :" + p.Item));
                PalletDetail detail1 = new PalletDetail();
                detail1.PalletId = pallet.Id;
                detail1.ReceiptDetailId = detail.Id;
                detail1.Quantity = p.Quantity;
                _context.PalletDetails.Add(detail1);
                ForkliftJob job = new ForkliftJob();
                job.jobType = JobType.Inbound;
                job.PalledId = pallet.Id;
                job.CreatedDate = DateTime.Now;
                job.CreatedUserId = GetUserId();
                job.JobStatus = JobStatus.Created;
                _context.ForkliftJobs.Add(job);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}