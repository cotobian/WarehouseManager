namespace WarehouseManager.ViewModels.Admin.Department
{
    public class TreeViewNode
    {
        public int Id { get; set; }
        public string text { get; set; }
        public List<TreeViewNode> nodes { get; set; }
    }
}