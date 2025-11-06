using EcommerceApp.Business.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceApp.Business.Interfaces
{
    public interface IArticuloService
    {
        Task<IEnumerable<ArticuloDto>> GetAllAsync();
        Task<ArticuloDto> GetByIdAsync(int id);
        Task<ArticuloDto> CreateAsync(ArticuloCreateDto dto);
        Task UpdateAsync(int id, ArticuloUpdateDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ArticuloDto>> GetByTiendaIdAsync(int tiendaId);
        Task<ArticuloTiendaDto> AsignarArticuloATiendaAsync(ArticuloTiendaCreateDto dto);
    }
}
