using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.ViewModels.Admin.RolePermission
{
    public class RolePermissionVm
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public int FunctionId { get; set; }

        public string FunctionName { get; set; }

        public Command Command { get; set; }
    }
}