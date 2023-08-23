namespace WarehouseManager.ViewModels.Warehouse.PalletDetail
{
    public class GetPalletDetailVm
    {
        public int PalletId { get; set; }
        public List<PartPallet>? ListPartPallet { get; set; }
    }

    public class PartPallet
    {
        public string PO { get; set; } = string.Empty;
        public string? Item { get; set; }
        public int Quantity { get; set; }
    }
}