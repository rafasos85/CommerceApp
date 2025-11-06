using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Entities
{
    public class CarritoItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CarritoId { get; set; }

        [Required]
        public int ArticuloId { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }

        // Navegación
        [ForeignKey("CarritoId")]
        public virtual Carrito Carrito { get; set; }

        [ForeignKey("ArticuloId")]
        public virtual Articulo Articulo { get; set; }
    }
}
