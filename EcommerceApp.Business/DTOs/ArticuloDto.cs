namespace EcommerceApp.Business.DTOs
{
    public class ArticuloDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }
    }

    public class ArticuloCreateDto
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public int Stock { get; set; }
    }

    public class ArticuloUpdateDto
    {
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }
    }

    public class ArticuloTiendaDto
    {
        public int Id { get; set; }
        public int ArticuloId { get; set; }
        public int TiendaId { get; set; }
        public int StockTienda { get; set; }
        public ArticuloDto Articulo { get; set; }
        public TiendaDto Tienda { get; set; }
    }

    public class ArticuloTiendaCreateDto
    {
        public int ArticuloId { get; set; }
        public int TiendaId { get; set; }
        public int StockTienda { get; set; }
    }
}
