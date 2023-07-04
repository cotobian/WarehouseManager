using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseManager.BackendServer.Constants;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("ReceiptOrders")]
    public class ReceiptOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string OrderNo { get; set; }

        [Required]
        public int AgentId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [MaxLength(20)]
        public string TruckNo { get; set; }

        [MaxLength(20)]
        public string CntrNo { get; set; }

        [MaxLength(20)]
        public string SealNo { get; set; }

        [MaxLength(50)]
        public string Booking { get; set; }

        public decimal Weight { get; set; } = 0;

        public decimal CBM { get; set; } = 0;

        [Required]
        public int UnitId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Commodity { get; set; }

        [MaxLength(50)]
        public string CustomDeclareNo { get; set; }

        public int CreatedUserId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Created;
    }
}