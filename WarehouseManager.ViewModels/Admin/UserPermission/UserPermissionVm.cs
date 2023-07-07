namespace WarehouseManager.ViewModels.Admin.UserPermission
{
    public class UserPermissionVm
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FunctionId { get; set; }
        public string FunctionName { get; set; } = "";
        public bool hasCreate { get; set; }
        public bool hasUpdate { get; set; }
        public bool hasDelete { get; set; }
        public bool hasView { get; set; }
        public bool hasApprove { get; set; }
    }
}