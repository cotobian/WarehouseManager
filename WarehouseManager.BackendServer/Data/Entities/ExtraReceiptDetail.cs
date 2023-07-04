using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("ExtraReceiptDetails")]
    public class ExtraReceiptDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ReceiptDetailId { get; set; }

        [Required, Column(TypeName = "nvarchar(MAX)")]
        public string Data { get; set; }

        public bool Status { get; set; } = true;
    }
}