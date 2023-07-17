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

        public int? ReceivedQuantity { get; set; }

        [Required]
        public int UnitId { get; set; }

        public decimal Length { get; set; } = 0;
        public decimal Width { get; set; } = 0;
        public decimal Height { get; set; } = 0;
        public decimal CBM { get; set; } = 0;

        [Column(TypeName = "nvarchar(200)")]
        public string? Note { get; set; }

        public bool Status { get; set; } = true;
    }
}