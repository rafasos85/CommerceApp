using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Entities
{
    public class ClienteArticulo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int ArticuloId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioCompra { get; set; }

        // Navegación
        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("ArticuloId")]
        public virtual Articulo Articulo { get; set; }
    }
}
