namespace Domain.Entities
{
    public class Produto
    {
        public Produto()
        {
            
        }
        public Produto(string descricacao, decimal valor, Categoria categoria)
        {            
        }
        public long Id { get; private set; }    
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public Categoria Categoria { get; private set; }  
    }
}
