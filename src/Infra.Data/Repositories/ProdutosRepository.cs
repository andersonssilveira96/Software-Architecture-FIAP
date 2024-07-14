using Domain.Entities;
using Domain.Repositories;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class ProdutosRepository : IProdutosRepository
    {
        private readonly TechChallengeContext _context;

        public ProdutosRepository(TechChallengeContext context)
        {
            _context = context;
        }

        public async Task<Produto> ObterProdutoPorId(long id) => await _context.Produto.FirstOrDefaultAsync(x => x.Id == id);
        public async Task<List<Produto>> ListarProdutos() => await _context.Produto.Include(x => x.Categoria).ToListAsync();
        public async Task<Produto> InserirProdutos(Produto produto)
        {
            if (produto is null)
            {
                throw new ArgumentNullException(nameof(produto));
            }

            _context.Produto.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> AtualizarProdutos(Produto produto)
        {
            _context.Produto.Entry(produto);          
            _context.Produto.Update(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task ExcluirProdutos(long id)
        {
            var entidade = _context.Produto.Find(id);
            
            if(entidade is not null)
                _context.Produto.Remove(entidade);

            await _context.SaveChangesAsync();           
        }

        public async Task<List<Produto>> ListarPorCategoria(long idCategoria)
        {
            var produtosCategoria =  _context.Produto.Include(x => x.Categoria).Where(x => x.Categoria.Id == idCategoria);
            return await produtosCategoria.ToListAsync();
        }
    }
}
