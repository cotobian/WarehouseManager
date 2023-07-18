namespace WarehouseManager.ViewModels.Warehouse.DeliveryOrder
{
    public class GetDeliveryOrderVm
    {
        public int Id { get; set; }

        public string OrderNo { get; set; } = string.Empty;

        public DateTime ExportDate { get; set; } = DateTime.Now;

        public string? DelivererName { get; set; }

        public string? OriginDO { get; set; }

        public string? TruckNo { get; set; }

        public string? CntrNo { get; set; }

        public string? SealNo { get; set; }

        public string? Inv { get; set; }

        public string? PhoneNumber { get; set; }

        public int? WarehouseDoorId { get; set; }

        public string? DoorName { get; set; }

        public DateTime? StartHour { get; set; }

        public DateTime? EndHour { get; set; }
    }
}