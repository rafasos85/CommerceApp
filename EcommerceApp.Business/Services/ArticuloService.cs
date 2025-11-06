using EcommerceApp.Business.DTOs;
using EcommerceApp.Business.Interfaces;
using EcommerceApp.Data;
using EcommerceApp.Data.Interfaces;
using EcommerceApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApp.Business.Services
{
    public class ArticuloService : IArticuloService
    {
        private readonly IArticuloRepository _articuloRepository;
        private readonly ApplicationDbContext _context;

        public ArticuloService(IArticuloRepository articuloRepository, ApplicationDbContext context)
        {
            _articuloRepository = articuloRepository;
            _context = context;
        }

        public async Task<IEnumerable<ArticuloDto>> GetAllAsync()
        {
            var articulos = await _articuloRepository.GetAllAsync();
            return articulos.Select(a => MapToDto(a));
        }

        public async Task<ArticuloDto> GetByIdAsync(int id)
        {
            var articulo = await _articuloRepository.GetByIdAsync(id);
            if (articulo == null)
                throw new Exception("Artículo no encontrado");

            return MapToDto(articulo);
        }

        public async Task<ArticuloDto> CreateAsync(ArticuloCreateDto dto)
        {
            var existingArticulo = await _articuloRepository.GetByCodigoAsync(dto.Codigo);
            if (existingArticulo != null)
                throw new Exception("El código de artículo ya existe");

            var articulo = new Articulo
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Imagen = dto.Imagen,
                Stock = dto.Stock,
                Activo = true
            };

            var created = await _articuloRepository.AddAsync(articulo);
            return MapToDto(created);
        }

        public async Task UpdateAsync(int id, ArticuloUpdateDto dto)
        {
            var articulo = await _articuloRepository.GetByIdAsync(id);
            if (articulo == null)
                throw new Exception("Artículo no encontrado");

            articulo.Descripcion = dto.Descripcion;
            articulo.Precio = dto.Precio;
            articulo.Imagen = dto.Imagen;
            articulo.Stock = dto.Stock;
            articulo.Activo = dto.Activo;

            await _articuloRepository.UpdateAsync(articulo);
        }

        public async Task DeleteAsync(int id)
        {
            var articulo = await _articuloRepository.GetByIdAsync(id);
            if (articulo == null)
                throw new Exception("Artículo no encontrado");

            articulo.Activo = false;
            await _articuloRepository.UpdateAsync(articulo);
        }

        public async Task<IEnumerable<ArticuloDto>> GetByTiendaIdAsync(int tiendaId)
        {
            var articulos = await _articuloRepository.GetByTiendaIdAsync(tiendaId);
            return articulos.Select(a => MapToDto(a));
        }

        public async Task<ArticuloTiendaDto> AsignarArticuloATiendaAsync(ArticuloTiendaCreateDto dto)
        {
            var articulo = await _articuloRepository.GetByIdAsync(dto.ArticuloId);
            if (articulo == null)
                throw new Exception("Artículo no encontrado");

            var tienda = await _context.Tiendas.FindAsync(dto.TiendaId);
            if (tienda == null)
                throw new Exception("Tienda no encontrada");

            var articuloTienda = new ArticuloTienda
            {
                ArticuloId = dto.ArticuloId,
                TiendaId = dto.TiendaId,
                StockTienda = dto.StockTienda,
                Fecha = DateTime.Now
            };

            await _context.ArticuloTiendas.AddAsync(articuloTienda);
            await _context.SaveChangesAsync();

            articuloTienda = await _context.ArticuloTiendas
                .Include(at => at.Articulo)
                .Include(at => at.Tienda)
                .FirstOrDefaultAsync(at => at.Id == articuloTienda.Id);

            return new ArticuloTiendaDto
            {
                Id = articuloTienda.Id,
                ArticuloId = articuloTienda.ArticuloId,
                TiendaId = articuloTienda.TiendaId,
                StockTienda = articuloTienda.StockTienda,
                Articulo = MapToDto(articuloTienda.Articulo),
                Tienda = new TiendaDto
                {
                    Id = articuloTienda.Tienda.Id,
                    Sucursal = articuloTienda.Tienda.Sucursal,
                    Direccion = articuloTienda.Tienda.Direccion,
                    Telefono = articuloTienda.Tienda.Telefono,
                    Activo = articuloTienda.Tienda.Activo
                }
            };
        }

        private ArticuloDto MapToDto(Articulo articulo)
        {
            return new ArticuloDto
            {
                Id = articulo.Id,
                Codigo = articulo.Codigo,
                Descripcion = articulo.Descripcion,
                Precio = articulo.Precio,
                Imagen = articulo.Imagen,
                Stock = articulo.Stock,
                Activo = articulo.Activo
            };
        }
    }
}
