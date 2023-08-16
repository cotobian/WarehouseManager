using System.ComponentModel.DataAnnotations;

namespace WarehouseManager.ViewModels.Warehouse.ReceiptDetail
{
    public class GetRemainVm
    {
        [Required]
        public string PO { get; set; } = string.Empty;

        public string? Item { get; set; }
    }
}