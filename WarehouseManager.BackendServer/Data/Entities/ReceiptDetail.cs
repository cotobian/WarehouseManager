using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("ReceiptDetails")]
    public class ReceiptDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int OrderId { get; set; }

        [Required, MaxLength(100)]
        public string PO { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Item { get; set; }

        [Required]
        public int ExpectedQuantity { get; set; }

        public int ReceivedQuantity { get; set; } = 0;

        [MaxLength(50)]
        public string? CustomDeclareNo { get; set; }

        [Required]
        public int UnitId { get; set; }

        [MaxLength(50)]
        public string? Size { get; set; }

        public decimal Weight { get; set; } = 0;

        public decimal CBM { get; set; } = 0;

        public bool Status { get; set; } = true;
    }
}