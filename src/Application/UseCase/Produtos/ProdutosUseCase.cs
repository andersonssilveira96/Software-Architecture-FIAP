using Application.DTOs.Produtos;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCase.Produtos
{
    public class ProdutosUseCase : IProdutosUseCase
    {
        private readonly IProdutosRepository _produtosRepository;
        private readonly IMapper _mapper;

        public ProdutosUseCase(IProdutosRepository produtosRepository, IMapper mapper)
        {
            _produtosRepository = produtosRepository;
            _mapper = mapper;
        }

        public async Task<ProdutoDto> Obter(long id)
        {
            return _mapper.Map<ProdutoDto>(await _produtosRepository.ObterProdutoPorId(id));
        }

        public async Task<List<ProdutoDto>> Listar()
        {
            return _mapper.Map<List<ProdutoDto>>(await _produtosRepository.ListarProdutos());
        }

        public async Task<List<ProdutoDto>> ListarPorCategoria(long idCategoria)
        {
            return _mapper.Map<List<ProdutoDto>>(await _produtosRepository.ListarPorCategoria(idCategoria));
        }

        public async Task<ProdutoDto> Cadastrar(CadastrarProdutoDto cadastrarProdutoDto)
        {
            var produto = _mapper.Map<Produto>(cadastrarProdutoDto);
            return _mapper.Map<ProdutoDto>(await _produtosRepository.InserirProdutos(produto));
        }

        public async Task<ProdutoDto> Atualizar(AtualizarProdutoDto atualizarProdutoDto)
        {
            var produto = _mapper.Map<Produto>(atualizarProdutoDto);

            return _mapper.Map<ProdutoDto>(await _produtosRepository.AtualizarProdutos(produto));
        }

        public async Task Excluir(long id)
        {
            await _produtosRepository.ExcluirProdutos(id);
        }
    }
}
