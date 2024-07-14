using Domain.Entities;
using Domain.Repositories;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly TechChallengeContext _context;
        public CategoriaRepository(TechChallengeContext context)
        {
            _context = context;
        }
        public async Task<Categoria> ObterPorId(long id) => await _context.Categoria.FirstOrDefaultAsync(x => x.Id == id);
    }
}
