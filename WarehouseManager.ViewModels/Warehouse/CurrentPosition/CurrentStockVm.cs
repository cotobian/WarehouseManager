namespace WarehouseManager.ViewModels.Warehouse.CurrentPosition
{
    public class CurrentStockVm
    {
        public string Bay { get; set; }
        public string Row { get; set; }
        public string Tier { get; set; }
        public string PO { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
    }
}