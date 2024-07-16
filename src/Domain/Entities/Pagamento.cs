using Domain.Enums;

namespace Domain.Entities
{
    public class Pagamento
    {
        public Pagamento()
        {            
        }
        public long Id { get; private set; }
        public string NumeroPagamento { get; private set; }
        public string QRCode { get; private set; }
        public PagamentoStatusEnum Status { get; private set; }
        public virtual Pedido Pedido { get; private set; }
        public long PedidoId { get; private set; }
        public void AdicionarPedido(Pedido pedido)
        {
            Pedido = pedido;    
            PedidoId = pedido.Id;
        }
    }
}
