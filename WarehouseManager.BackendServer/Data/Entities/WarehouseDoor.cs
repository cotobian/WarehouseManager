using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("WarehouseDoors")]
    public class WarehouseDoor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int WarehouseId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string DoorNo { get; set; }

        public bool Status { get; set; } = true;
    }
}