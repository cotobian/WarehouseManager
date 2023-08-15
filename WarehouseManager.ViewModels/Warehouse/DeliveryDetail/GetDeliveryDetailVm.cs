namespace WarehouseManager.ViewModels.Warehouse.DeliveryDetail
{
    public class GetDeliveryDetailVm
    {
        public int Id { get; set; }

        public int DeliveryOrderId { get; set; }

        public int? ReceiptDetailId { get; set; }

        public string? PO { get; set; }

        public string? Item { get; set; }

        public int Quantity { get; set; }

        public int? PositionId { get; set; }

        public string? PositionName { get; set; }

        public string? Size { get; set; }

        public decimal? CBM { get; set; }

        public decimal? Weight { get; set; }

        public bool Status { get; set; } = true;
    }
}