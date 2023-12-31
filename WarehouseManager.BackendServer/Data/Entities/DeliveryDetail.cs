﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManager.BackendServer.Data.Entities
{
    [Table("DeliveryDetails")]
    public class DeliveryDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int DeliveryOrderId { get; set; }

        public int? ReceiptDetailId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int? PositionId { get; set; }

        public string? Size { get; set; }

        public decimal? CBM { get; set; }

        public decimal? Weight { get; set; }

        public bool Status { get; set; } = true;
    }
}