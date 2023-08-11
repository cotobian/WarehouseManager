namespace WarehouseManager.ViewModels.Warehouse.CurrentPosition
{
    public class StackLayoutVm
    {
        public string Bay { get; set; } = string.Empty;
        public List<RowDisplay>? RowList { get; set; }
    }

    public class RowDisplay
    {
        public string? RowText { get; set; }
        public string? RowColor { get; set; }
    }
}