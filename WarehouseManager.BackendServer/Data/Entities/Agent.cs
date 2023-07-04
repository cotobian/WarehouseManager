using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("Agents")]
    public class Agent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }

        [Required, Column(TypeName = "nvarchar(20)")]
        public string ShortName { get; set; }

        public bool Status { get; set; } = true;
    }
}