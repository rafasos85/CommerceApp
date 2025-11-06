using EcommerceApp.Business.DTOs;
using EcommerceApp.Business.Interfaces;
using EcommerceApp.Data.Interfaces;
using EcommerceApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApp.Business.Services
{
    public class TiendaService : ITiendaService
    {
        private readonly ITiendaRepository _tiendaRepository;

        public TiendaService(ITiendaRepository tiendaRepository)
        {
            _tiendaRepository = tiendaRepository;
        }

        public async Task<IEnumerable<TiendaDto>> GetAllAsync()
        {
            var tiendas = await _tiendaRepository.GetAllAsync();
            return tiendas.Select(t => MapToDto(t));
        }

        public async Task<TiendaDto> GetByIdAsync(int id)
        {
            var tienda = await _tiendaRepository.GetByIdAsync(id);
            if (tienda == null)
                throw new Exception("Tienda no encontrada");

            return MapToDto(tienda);
        }

        public async Task<TiendaDto> CreateAsync(TiendaCreateDto dto)
        {
            var tienda = new Tienda
            {
                Sucursal = dto.Sucursal,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Activo = true
            };

            var created = await _tiendaRepository.AddAsync(tienda);
            return MapToDto(created);
        }

        public async Task UpdateAsync(int id, TiendaUpdateDto dto)
        {
            var tienda = await _tiendaRepository.GetByIdAsync(id);
            if (tienda == null)
                throw new Exception("Tienda no encontrada");

            tienda.Sucursal = dto.Sucursal;
            tienda.Direccion = dto.Direccion;
            tienda.Telefono = dto.Telefono;
            tienda.Activo = dto.Activo;

            await _tiendaRepository.UpdateAsync(tienda);
        }

        public async Task DeleteAsync(int id)
        {
            var tienda = await _tiendaRepository.GetByIdAsync(id);
            if (tienda == null)
                throw new Exception("Tienda no encontrada");

            tienda.Activo = false;
            await _tiendaRepository.UpdateAsync(tienda);
        }

        private TiendaDto MapToDto(Tienda tienda)
        {
            return new TiendaDto
            {
                Id = tienda.Id,
                Sucursal = tienda.Sucursal,
                Direccion = tienda.Direccion,
                Telefono = tienda.Telefono,
                Activo = tienda.Activo
            };
        }
    }
}
