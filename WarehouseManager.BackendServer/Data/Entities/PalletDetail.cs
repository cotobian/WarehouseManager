using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("PalletDetails")]
    public class PalletDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PalletId { get; set; }

        [Required]
        public int ReceiptDetailId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}