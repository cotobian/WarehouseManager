﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("DeliveryOrders")]
    public class DeliveryOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string OrderNo { get; set; } = string.Empty;

        public DateTime ExportDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(50)")]
        public string? DelivererName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? OriginDO { get; set; }

        [MaxLength(20)]
        public string? TruckNo { get; set; }

        [MaxLength(20)]
        public string? CntrNo { get; set; }

        [MaxLength(20)]
        public string? SealNo { get; set; }

        [MaxLength(20)]
        public string? Inv { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        public int WarehouseDoorId { get; set; }
        public DateTime? StartHour { get; set; }
        public DateTime? EndHour { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Created;
    }
}