using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Entities
{
    public class ArticuloTienda
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ArticuloId { get; set; }

        [Required]
        public int TiendaId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public int StockTienda { get; set; }

        // Navegación
        [ForeignKey("ArticuloId")]
        public virtual Articulo Articulo { get; set; }

        [ForeignKey("TiendaId")]
        public virtual Tienda Tienda { get; set; }
    }
}
