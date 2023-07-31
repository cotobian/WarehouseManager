namespace WarehouseManager.ViewModels.Index
{
    public class GetWarehousePositionVm
    {
        public int Id { get; set; }

        public int WarehouseId { get; set; }

        public string WarehouseName { get; set; }

        public string Bay { get; set; }

        public string Row { get; set; }

        public string Tier { get; set; }

        public bool Status { get; set; } = true;
    }
}