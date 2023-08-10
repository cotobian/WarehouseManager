namespace WarehouseManager.ViewModels.Warehouse.CurrentPosition
{
    public class StackLayoutVm
    {
        public string Bay { get; set; } = string.Empty;
        public List<string>? RowList { get; set; }
        public string? Color { get; set; }
    }
}