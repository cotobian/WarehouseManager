using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("TallyJobs")]
    public class TallyJob
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ReceiptDetailId { get; set; }

        public int? PalletId { get; set; }

        public int? Quantity { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CompletedUserId { get; set; }

        public DateTime? CompletedDate { get; set; }

        public JobType jobType { get; set; } = JobType.Inbound;

        public JobStatus jobStatus { get; set; } = JobStatus.Created;
    }
}