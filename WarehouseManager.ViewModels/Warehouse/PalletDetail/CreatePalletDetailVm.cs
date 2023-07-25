namespace WarehouseManager.ViewModels.Warehouse.PalletDetail
{
    public class CreatePalletDetailVm
    {
        public string PO { get; set; } = string.Empty;
        public string? Item { get; set; }
        public string PalletNo { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}