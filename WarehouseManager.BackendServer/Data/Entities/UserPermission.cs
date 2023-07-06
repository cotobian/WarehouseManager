using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("UserPermissions")]
    public class UserPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int FunctionId { get; set; }

        [Required]
        public Command Command { get; set; }

        public bool Status { get; set; } = true;
    }
}