using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("WarehousePositions")]
    public class WarehousePosition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int WarehouseId { get; set; }

        [Required, MaxLength(10)]
        public string Bay { get; set; }

        [Required, MaxLength(10)]
        public string Row { get; set; }

        [Required, MaxLength(10)]
        public string Tier { get; set; }

        public bool Status { get; set; } = true;
    }
}