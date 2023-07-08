namespace WarehouseManager.ViewModels.Admin.User
{
    public class GetUserVm
    {
        public int Id { get; set; }

        public string FullName { get; set; } = "";

        public string Username { get; set; } = "";

        public int RoleId { get; set; }

        public string RoleName { get; set; } = "";

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = "";
    }
}