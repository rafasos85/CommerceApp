using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Entities
{
    public class Carrito
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime? FechaCompra { get; set; }

        public bool Completado { get; set; } = false;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        // Navegación
        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        public virtual ICollection<CarritoItem> Items { get; set; }
    }
}
