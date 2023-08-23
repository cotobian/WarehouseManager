namespace WarehouseManager.ViewModels.Warehouse.Pallet
{
    public class GetPalletVm
    {
        public int Id { get; set; }

        public string PalletNo { get; set; } = string.Empty;

        public string? StatusText { get; set; }

        public string? CreatedDateText { get; set; }
    }
}