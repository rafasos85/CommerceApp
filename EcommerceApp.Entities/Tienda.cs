using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Entities
{
    public class Tienda
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Sucursal { get; set; }

        [Required]
        [MaxLength(250)]
        public string Direccion { get; set; }

        [MaxLength(20)]
        public string Telefono { get; set; }

        public bool Activo { get; set; } = true;

        // Relaciones
        public virtual ICollection<ArticuloTienda> ArticuloTiendas { get; set; }
    }
}
