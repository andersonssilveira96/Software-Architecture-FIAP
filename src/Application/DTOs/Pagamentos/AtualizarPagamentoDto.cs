namespace Application.DTOs.Pagamentos
{
    public class AtualizarPagamentoDto
    {
        public Guid NumeroPagamento { get; set; }
        public bool Aprovado { get; set; }
    }
}
