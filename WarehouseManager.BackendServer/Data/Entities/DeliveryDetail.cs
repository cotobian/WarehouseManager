using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("DeliveryDetails")]
    public class DeliveryDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ReceiptDetailId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int FactoryId { get; set; }

        [Required]
        public string PickUpPosition { get; set; }

        public decimal CBM { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Note { get; set; }

        public bool Status { get; set; } = true;
    }
}