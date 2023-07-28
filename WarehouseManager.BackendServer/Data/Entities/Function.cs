using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("Functions")]
    public class Function
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string? Name { get; set; }

        [MaxLength(200)]
        public string? Url { get; set; }

        [MaxLength(30)]
        public string? Controller { get; set; }

        [MaxLength(30)]
        public string? Icon { get; set; }

        public int? SortOrder { get; set; }
        public int? ParentId { get; set; }

        public bool Status { get; set; } = true;
    }
}