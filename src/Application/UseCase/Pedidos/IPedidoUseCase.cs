using Application.DTOs.Pedido;
using Domain.Enums;

namespace Application.UseCase.Pedidos
{
    public interface IPedidoUseCase
    {
        Task<PedidoDto> Inserir(CadastrarPedidoDto pedidoDto);
        Task<PedidoDto> EnviarPagamento(long id);
        Task<PedidoDto> AtualizarStatus(long id, int status);
        Task<IEnumerable<PedidoDto>> Listar();
    }
}
