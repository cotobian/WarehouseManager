using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseManager.BackendServer.Constants;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("ForkliftJobs")]
    public class ForkliftJob
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public ForkLiftJobType JobType { get; set; } = ForkLiftJobType.Inbound;

        [Required]
        public int PalledId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int PositionId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime CompletedDate { get; set; }
        public int CreatedUserId { get; set; }
        public int CompletedUserId { get; set; }
        public ForkLiftJobStatus JobStatus { get; set; } = ForkLiftJobStatus.Created;
    }
}