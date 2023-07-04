using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseManager.BackendServer.Constants;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("CurrentPositions")]
    public class CurrentPosition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PositionId { get; set; }

        public int? ReceiptDetailId { get; set; }
        public int? Quantity { get; set; }
        public CurrentPositionStatus Status { get; set; } = CurrentPositionStatus.Empty;
    }
}