using EcommerceApp.Business.DTOs;
using EcommerceApp.Business.Interfaces;
using EcommerceApp.Data;
using EcommerceApp.Data.Interfaces;
using EcommerceApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApp.Business.Services
{
    public class CarritoService : ICarritoService
    {
        private readonly ICarritoRepository _carritoRepository;
        private readonly IArticuloRepository _articuloRepository;
        private readonly ApplicationDbContext _context;

        public CarritoService(
            ICarritoRepository carritoRepository,
            IArticuloRepository articuloRepository,
            ApplicationDbContext context)
        {
            _carritoRepository = carritoRepository;
            _articuloRepository = articuloRepository;
            _context = context;
        }

        public async Task<CarritoDto> GetCarritoActivoAsync(int clienteId)
        {
            var carrito = await _carritoRepository.GetCarritoActivoByClienteIdAsync(clienteId);
            
            if (carrito == null)
            {
                carrito = new Carrito
                {
                    ClienteId = clienteId,
                    FechaCreacion = DateTime.Now,
                    Completado = false,
                    Total = 0
                };
                carrito = await _carritoRepository.AddAsync(carrito);
            }

            return MapToDto(carrito);
        }

        public async Task<CarritoDto> AgregarItemAsync(int clienteId, AgregarItemCarritoDto dto)
        {
            var carrito = await _carritoRepository.GetCarritoActivoByClienteIdAsync(clienteId);
            
            if (carrito == null)
            {
                carrito = new Carrito
                {
                    ClienteId = clienteId,
                    FechaCreacion = DateTime.Now,
                    Completado = false,
                    Total = 0
                };
                carrito = await _carritoRepository.AddAsync(carrito);
            }

            var articulo = await _articuloRepository.GetByIdAsync(dto.ArticuloId);
            if (articulo == null)
                throw new Exception("Artículo no encontrado");

            if (articulo.Stock < dto.Cantidad)
                throw new Exception("Stock insuficiente");

            var itemExistente = carrito.Items?.FirstOrDefault(i => i.ArticuloId == dto.ArticuloId);
            
            if (itemExistente != null)
            {
                itemExistente.Cantidad += dto.Cantidad;
                itemExistente.Subtotal = itemExistente.Cantidad * itemExistente.PrecioUnitario;
                _context.CarritoItems.Update(itemExistente);
            }
            else
            {
                var nuevoItem = new CarritoItem
                {
                    CarritoId = carrito.Id,
                    ArticuloId = dto.ArticuloId,
                    Cantidad = dto.Cantidad,
                    PrecioUnitario = articulo.Precio,
                    Subtotal = dto.Cantidad * articulo.Precio
                };
                await _context.CarritoItems.AddAsync(nuevoItem);
            }

            carrito.Total = await CalcularTotalCarritoAsync(carrito.Id);
            _context.Carritos.Update(carrito);
            await _context.SaveChangesAsync();

            carrito = await _carritoRepository.GetCarritoWithItemsAsync(carrito.Id);
            return MapToDto(carrito);
        }

        public async Task<CarritoDto> ActualizarItemAsync(int clienteId, ActualizarItemCarritoDto dto)
        {
            var carrito = await _carritoRepository.GetCarritoActivoByClienteIdAsync(clienteId);
            if (carrito == null)
                throw new Exception("Carrito no encontrado");

            var item = carrito.Items?.FirstOrDefault(i => i.Id == dto.ItemId);
            if (item == null)
                throw new Exception("Item no encontrado en el carrito");

            var articulo = await _articuloRepository.GetByIdAsync(item.ArticuloId);
            if (articulo.Stock < dto.Cantidad)
                throw new Exception("Stock insuficiente");

            item.Cantidad = dto.Cantidad;
            item.Subtotal = item.Cantidad * item.PrecioUnitario;
            _context.CarritoItems.Update(item);

            carrito.Total = await CalcularTotalCarritoAsync(carrito.Id);
            _context.Carritos.Update(carrito);
            await _context.SaveChangesAsync();

            carrito = await _carritoRepository.GetCarritoWithItemsAsync(carrito.Id);
            return MapToDto(carrito);
        }

        public async Task RemoverItemAsync(int clienteId, int itemId)
        {
            var carrito = await _carritoRepository.GetCarritoActivoByClienteIdAsync(clienteId);
            if (carrito == null)
                throw new Exception("Carrito no encontrado");

            var item = carrito.Items?.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                throw new Exception("Item no encontrado en el carrito");

            _context.CarritoItems.Remove(item);

            carrito.Total = await CalcularTotalCarritoAsync(carrito.Id);
            _context.Carritos.Update(carrito);
            await _context.SaveChangesAsync();
        }

        public async Task<CarritoDto> CompletarCompraAsync(int clienteId)
        {
            var carrito = await _carritoRepository.GetCarritoActivoByClienteIdAsync(clienteId);
            if (carrito == null || !carrito.Items.Any())
                throw new Exception("El carrito está vacío");

            // Verificar stock y actualizar
            foreach (var item in carrito.Items)
            {
                var articulo = await _articuloRepository.GetByIdAsync(item.ArticuloId);
                if (articulo.Stock < item.Cantidad)
                    throw new Exception($"Stock insuficiente para {articulo.Descripcion}");

                articulo.Stock -= item.Cantidad;
                await _articuloRepository.UpdateAsync(articulo);

                // Crear registro de compra
                var clienteArticulo = new ClienteArticulo
                {
                    ClienteId = clienteId,
                    ArticuloId = item.ArticuloId,
                    Fecha = DateTime.Now,
                    Cantidad = item.Cantidad,
                    PrecioCompra = item.PrecioUnitario
                };
                await _context.ClienteArticulos.AddAsync(clienteArticulo);
            }

            carrito.Completado = true;
            carrito.FechaCompra = DateTime.Now;
            await _carritoRepository.UpdateAsync(carrito);

            return MapToDto(carrito);
        }

        private async Task<decimal> CalcularTotalCarritoAsync(int carritoId)
        {
            var items = await _context.CarritoItems
                .Where(i => i.CarritoId == carritoId)
                .ToListAsync();

            return items.Sum(i => i.Subtotal);
        }

        private CarritoDto MapToDto(Carrito carrito)
        {
            return new CarritoDto
            {
                Id = carrito.Id,
                ClienteId = carrito.ClienteId,
                FechaCreacion = carrito.FechaCreacion,
                Total = carrito.Total,
                Items = carrito.Items?.Select(i => new CarritoItemDto
                {
                    Id = i.Id,
                    ArticuloId = i.ArticuloId,
                    ArticuloDescripcion = i.Articulo?.Descripcion,
                    Cantidad = i.Cantidad,
                    PrecioUnitario = i.PrecioUnitario,
                    Subtotal = i.Subtotal
                }).ToList()
            };
        }
    }
}
