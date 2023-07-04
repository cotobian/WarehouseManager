namespace WarehouseManager.ViewModels.Admin.User
{
    public class GetUserVm
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string RoleName { get; set; }

        public string DepartmentName { get; set; }
    }
}