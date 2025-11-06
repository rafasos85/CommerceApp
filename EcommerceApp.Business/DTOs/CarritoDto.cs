using System;
using System.Collections.Generic;

namespace EcommerceApp.Business.DTOs
{
    public class CarritoDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal Total { get; set; }
        public List<CarritoItemDto> Items { get; set; }
    }

    public class CarritoItemDto
    {
        public int Id { get; set; }
        public int ArticuloId { get; set; }
        public string ArticuloDescripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class AgregarItemCarritoDto
    {
        public int ArticuloId { get; set; }
        public int Cantidad { get; set; }
    }

    public class ActualizarItemCarritoDto
    {
        public int ItemId { get; set; }
        public int Cantidad { get; set; }
    }
}
