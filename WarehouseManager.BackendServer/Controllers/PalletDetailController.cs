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
            Pallet pallet = await _context.Pallets.Where(c => c.PalletNo == palletList[0].PalletNo && c.Status == PalletStatus.Printed).FirstOrDefaultAsync();
            if (pallet == null) return BadRequest("Cannot find Pallet Number: " + palletList[0].PalletNo);
            foreach (CreatePalletDetailVm p in palletList)
            {
                ReceiptDetail detail = (from rd in _context.ReceiptDetails
                                        join ro in _context.ReceiptOrders
                                        on rd.OrderId equals ro.Id
                                        where rd.PO == p.PO && (string.IsNullOrEmpty(p.Item) || rd.Item == p.Item)
                                        && (ro.OrderStatus == OrderStatus.Created || ro.OrderStatus == OrderStatus.Processing)
                                        select rd).FirstOrDefault();
                if (detail == null) return BadRequest("Cannot find PO: " + p.PO + (string.IsNullOrEmpty(p.Item) ? "" : "Item :" + p.Item));
                detail.ReceivedQuantity += p.Quantity;
                if (detail.ReceivedQuantity > detail.ExpectedQuantity) return BadRequest("Wrong Receive Number!");

                PalletDetail palletDetail = new PalletDetail();
                palletDetail.PalletId = pallet.Id;
                palletDetail.ReceiptDetailId = detail.Id;
                palletDetail.Quantity = p.Quantity;
                _context.PalletDetails.Add(palletDetail);

                ForkliftJob job = new ForkliftJob();
                job.jobType = JobType.Inbound;
                job.PalledId = pallet.Id;
                job.CreatedDate = DateTime.Now;
                job.CreatedUserId = GetUserId();
                job.JobStatus = JobStatus.Created;
                _context.ForkliftJobs.Add(job);

                TallyJob tallyJob = new TallyJob();
                tallyJob.jobType = JobType.Inbound;
                tallyJob.CompletedDate = DateTime.Now;
                tallyJob.CreatedDate = DateTime.Now;
                tallyJob.CreatedUserId = GetUserId();
                tallyJob.CompletedUserId = GetUserId();
                tallyJob.PalletId = pallet.Id;
                tallyJob.ReceiptDetailId = detail.Id;
                tallyJob.jobStatus = JobStatus.Completed;
                _context.TallyJobs.Add(tallyJob);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("GetPalletDetail/{palletId}")]
        public async Task<IActionResult> GetPalletDetail(int palletId)
        {
            List<PalletDetail> detailList = await _context.PalletDetails.Where(c => c.PalletId == palletId).ToListAsync();
            GetPalletDetailVm getPallet = new GetPalletDetailVm();
            getPallet.PalletId = palletId;
            foreach (PalletDetail detail in detailList)
            {
                PartPallet part = new PartPallet();
                ReceiptDetail receipt = await _context.ReceiptDetails.Where(c => c.Id == detail.ReceiptDetailId).FirstOrDefaultAsync();
                part.PO = receipt.PO;
                part.Item = receipt.Item;
                part.Quantity = detail.Quantity;
                getPallet.ListPartPallet.Add(part);
            }
            return Ok(getPallet);
        }
    }
}