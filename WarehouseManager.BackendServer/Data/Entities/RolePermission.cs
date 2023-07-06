using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("RolePermissions")]
    public class RolePermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int FunctionId { get; set; }

        [Required]
        public Command Command { get; set; }

        public bool Status { get; set; } = true;
    }
}