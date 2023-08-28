using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels.Constants;
using WarehouseManager.ViewModels.Warehouse.DeliveryDetail;

namespace WarehouseManager.BackendServer.Controllers
{
    public class DeliveryDetailController : BaseController<DeliveryDetail>
    {
        public DeliveryDetailController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        [HttpGet("orderid/{orderid}")]
        public async Task<IActionResult> GetByOrderId(int orderid)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var sql = @"select dd.*,CONCAT(wp.Bay,'.',wp.Row,'.',wp.Tier) as PositionName,
                rd.PO,rd.Item from DeliveryDetails dd left join WarehousePositions wp
                on dd.PositionId=wp.Id join ReceiptDetails rd on rd.Id=dd.ReceiptDetailId where
                DeliveryOrderId=@OrderId and dd.Status=1";
                var parameters = new { OrderId = orderid };
                var result = await conn.QueryAsync<GetDeliveryDetailVm>(sql, parameters, null, 120, CommandType.Text);
                return Ok(result.ToList());
            }
        }

        [HttpPost("AddDeliveryVm")]
        public async Task<IActionResult> PostVm([FromBody] GetDeliveryDetailVm item)
        {
            ReceiptDetail receiptDetail = await _context.ReceiptDetails
                .Where(c => c.PO.Equals(item.PO) && (string.IsNullOrEmpty(c.Item) || c.Item.Equals(item.Item)))
                .FirstOrDefaultAsync();
            if (receiptDetail == null) return BadRequest("PO and Item Not found");
            PalletDetail palletDetail = await _context.PalletDetails
                .Where(c => c.ReceiptDetailId == receiptDetail.Id)
                .FirstOrDefaultAsync();
            if (palletDetail == null) return BadRequest("Pallet Detail Not found");
            CurrentPosition currentPosition = await _context.CurrentPositions
                .Where(c => c.PalletId == palletDetail.PalletId && c.Status == CurrentPositionStatus.Occupied)
                .FirstOrDefaultAsync();
            if (currentPosition == null) return BadRequest("Pallet Stacked Not found");
            item.PositionId = currentPosition.PositionId;
            item.ReceiptDetailId = receiptDetail.Id;
            DeliveryDetail deliveryDetail = convertGetVm(item);
            _context.DeliveryDetails.Add(deliveryDetail);
            await _context.SaveChangesAsync();

            ForkliftJob forkliftJob = new ForkliftJob();
            forkliftJob.jobType = JobType.Outbound;
            forkliftJob.CreatedDate = DateTime.Now;
            forkliftJob.CreatedUserId = GetUserId();
            forkliftJob.PalledId = palletDetail.PalletId;
            forkliftJob.PositionId = currentPosition.PositionId;
            forkliftJob.JobStatus = JobStatus.Created;
            _context.ForkliftJobs.Add(forkliftJob);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("UploadExcel")]
        public async Task<IActionResult> UploadExcel([FromBody] List<List<string>> uploadedData)
        {
            foreach (List<string> item in uploadedData)
            {
                DeliveryDetail deliveryDetail = new DeliveryDetail();
                string PO = item[0];
                string? Item = item[1];
                ReceiptDetail receiptDetail = await _context.ReceiptDetails
                    .Where(c => c.PO.Equals(PO) && (string.IsNullOrEmpty(c.Item) || c.Item.Equals(Item)))
                    .FirstOrDefaultAsync();
                if (receiptDetail == null) return BadRequest("PO and Item Not found");
                PalletDetail palletDetail = await _context.PalletDetails
                    .Where(c => c.ReceiptDetailId == receiptDetail.Id)
                    .FirstOrDefaultAsync();
                if (palletDetail == null) return BadRequest("Pallet Detail Not found");
                CurrentPosition currentPosition = await _context.CurrentPositions
                    .Where(c => c.PalletId == palletDetail.PalletId && c.Status == CurrentPositionStatus.Occupied)
                    .FirstOrDefaultAsync();
                if (currentPosition == null) return BadRequest("Pallet Stacked Not found");
                deliveryDetail.ReceiptDetailId = receiptDetail.Id;
                deliveryDetail.PositionId = currentPosition.Id;
                deliveryDetail.Quantity = int.Parse(item[2]);
                deliveryDetail.Weight = int.Parse(item[3]);
                deliveryDetail.CBM = int.Parse(item[4]);
                deliveryDetail.Size = item[5];
                deliveryDetail.Status = true;
                _context.DeliveryDetails.Add(deliveryDetail);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        private DeliveryDetail convertGetVm(GetDeliveryDetailVm vm)
        {
            return new DeliveryDetail
            {
                Id = vm.Id,
                DeliveryOrderId = vm.DeliveryOrderId,
                ReceiptDetailId = vm.ReceiptDetailId,
                Quantity = vm.Quantity,
                PositionId = vm.PositionId,
                Size = vm.Size,
                CBM = vm.CBM,
                Weight = vm.Weight,
                Status = vm.Status
            };
        }
    }
}