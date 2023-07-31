namespace WarehouseManager.ViewModels.Admin.UserPermission
{
    public class CreateUserPermissionVm
    {
        public int FunctionId { get; set; }
        public int UserId { get; set; }
        public string Command { get; set; } = String.Empty;
    }
}