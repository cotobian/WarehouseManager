namespace WarehouseManager.ViewModels.Warehouse.PalletDetail
{
    public class GetPalletDetailVm
    {
        public string PO { get; set; } = string.Empty;
        public string? Item { get; set; }
        public int Quantity { get; set; }
    }
}