using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string FullName { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public byte[] PasswordHash { get; set; }

        [Required]
        [MaxLength(150)]
        public byte[] PasswordSalt { get; set; }

        public int RoleId { get; set; }

        public int DepartmentId { get; set; }

        public UserStatus Status { get; set; } = UserStatus.Working;
    }
}