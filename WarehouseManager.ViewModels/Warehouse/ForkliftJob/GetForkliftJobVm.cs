using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.ViewModels.Warehouse.ForkliftJob
{
    public class GetForkliftJobVm
    {
        public int Id { get; set; }
        public int PalledId { get; set; }
        public string PalletNo { get; set; } = string.Empty;
        public string? PositionName { get; set; }
        public int? PositionId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedDateText { get; set; } = string.Empty;
        public DateTime? CompletedDate { get; set; }
        public string? CompletedDateText { get; set; }
        public int CreatedUserId { get; set; }
        public string CreatedUserName { get; set; } = string.Empty;
        public int? CompletedUserId { get; set; }
        public string? CompletedUserName { get; set; }
        public JobStatus JobStatus { get; set; }
        public string JobStatusText { get; set; } = string.Empty;
        public JobType jobType { get; set; }
        public string jobTypeText { get; set; } = string.Empty;
    }
}