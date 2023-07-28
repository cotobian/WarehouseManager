using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseManager.ViewModels.Constants;

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

        public int? PalletId { get; set; }
        public CurrentPositionStatus Status { get; set; } = CurrentPositionStatus.Empty;
    }
}