using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPagamentoRepository
    {
        Task<Pagamento> ObterPorGUID(Guid numeroPagamento);
        Task<Pagamento> ObterPorId(long id);
        Task<Pagamento> Inserir(Pagamento pagamento);
        Task<Pagamento> Atualizar(Pagamento pagamento);
    }
}
