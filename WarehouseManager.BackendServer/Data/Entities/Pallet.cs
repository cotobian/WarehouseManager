using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("Pallets")]
    public class Pallet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string PalletNo { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        public int ReceiptDetailId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public bool Status { get; set; } = true;
    }
}