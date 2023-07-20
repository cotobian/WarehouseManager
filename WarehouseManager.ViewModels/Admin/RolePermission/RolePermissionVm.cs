﻿namespace WarehouseManager.ViewModels.Admin.RolePermission
{
    public class RolePermissionVm
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int FunctionId { get; set; }
        public string FunctionName { get; set; } = "";
        public bool hasCreate { get; set; }
        public bool hasUpdate { get; set; }
        public bool hasDelete { get; set; }
        public bool hasView { get; set; }
        public bool hasApprove { get; set; }
    }
}