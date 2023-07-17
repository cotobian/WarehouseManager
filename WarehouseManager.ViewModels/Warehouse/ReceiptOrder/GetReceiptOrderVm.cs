using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.ViewModels.Warehouse.ReceiptOrder
{
    public class GetReceiptOrderVm
    {
        public int Id { get; set; }
        public string OrderNo { get; set; } = string.Empty;
        public int? AgentId { get; set; }
        public string AgentName { get; set; } = string.Empty;

        public int? CustomerId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string? TruckNo { get; set; }

        public string? CntrNo { get; set; }

        public string? SealNo { get; set; }

        public string? Booking { get; set; }

        public decimal Weight { get; set; } = 0;

        public decimal CBM { get; set; } = 0;

        public string? Commodity { get; set; }

        public string? CustomDeclareNo { get; set; }

        public int CreatedUserId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public OrderStatus OrderStatus { get; set; }
    }
}