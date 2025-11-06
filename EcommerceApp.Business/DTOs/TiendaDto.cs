namespace EcommerceApp.Business.DTOs
{
    public class TiendaDto
    {
        public int Id { get; set; }
        public string Sucursal { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; }
    }

    public class TiendaCreateDto
    {
        public string Sucursal { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }

    public class TiendaUpdateDto
    {
        public string Sucursal { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; }
    }
}
