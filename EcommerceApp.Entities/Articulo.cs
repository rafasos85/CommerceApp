using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Entities
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Codigo { get; set; }

        [Required]
        [MaxLength(200)]
        public string Descripcion { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [MaxLength(500)]
        public string Imagen { get; set; }

        [Required]
        public int Stock { get; set; }

        public bool Activo { get; set; } = true;

        // Relaciones
        public virtual ICollection<ArticuloTienda> ArticuloTiendas { get; set; }
        public virtual ICollection<ClienteArticulo> ClienteArticulos { get; set; }
        public virtual ICollection<CarritoItem> CarritoItems { get; set; }
    }
}
