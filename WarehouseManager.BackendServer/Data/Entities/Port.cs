using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("Ports")]
    public class Port
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }

        [Required, MaxLength(20)]
        public string PortCode { get; set; }

        public bool Status { get; set; } = true;
    }
}