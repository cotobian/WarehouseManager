using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("Factories")]
    public class Factory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(500)")]
        public string Address { get; set; } = string.Empty;

        public bool Status { get; set; } = true;
    }
}