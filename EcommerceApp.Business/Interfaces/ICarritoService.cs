using EcommerceApp.Business.DTOs;
using System.Threading.Tasks;

namespace EcommerceApp.Business.Interfaces
{
    public interface ICarritoService
    {
        Task<CarritoDto> GetCarritoActivoAsync(int clienteId);
        Task<CarritoDto> AgregarItemAsync(int clienteId, AgregarItemCarritoDto dto);
        Task<CarritoDto> ActualizarItemAsync(int clienteId, ActualizarItemCarritoDto dto);
        Task RemoverItemAsync(int clienteId, int itemId);
        Task<CarritoDto> CompletarCompraAsync(int clienteId);
    }
}
