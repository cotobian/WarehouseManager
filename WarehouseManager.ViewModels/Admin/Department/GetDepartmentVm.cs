namespace WarehouseManager.ViewModels.Admin.Department
{
    public class GetDepartmentVm
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }
}