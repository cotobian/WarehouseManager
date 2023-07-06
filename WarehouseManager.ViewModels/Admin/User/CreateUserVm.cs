namespace WarehouseManager.ViewModels.Admin.User
{
    public class CreateUserVm
    {
        public string FullName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        public int DepartmentId { get; set; }
    }
}