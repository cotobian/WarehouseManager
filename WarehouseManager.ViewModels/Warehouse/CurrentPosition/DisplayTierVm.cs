namespace WarehouseManager.ViewModels.Warehouse.CurrentPosition
{
    public class DisplayTierVm
    {
        public string PO { get; set; } = string.Empty;
        public string? Item { get; set; }
        public string Tier { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}