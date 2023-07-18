using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManager.ViewModels.Warehouse.ReceiptDetail
{
    public class GetReceiptDetailVm
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string PO { get; set; } = string.Empty;

        public string? Item { get; set; }

        public int? ExpectedQuantity { get; set; }

        public int? ReceivedQuantity { get; set; }

        public string? CustomDeclareNo { get; set; }

        public int? UnitId { get; set; }

        public string? UnitName { get; set; }

        public string? Size { get; set; }

        public decimal? Weight { get; set; } = 0;

        public decimal? CBM { get; set; } = 0;

        public bool Status { get; set; } = true;
    }
}