﻿using Application.DTOs;
using Application.DTOs.Pedido;
using Application.UseCase.Pagamentos;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;

namespace Application.UseCase.Pedidos
{
    public class PedidoUseCase : IPedidoUseCase
    {
        private readonly IPedidoRepository _repository;
        private readonly IProdutosRepository _produtoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        private readonly IPagamentoUseCase _pagamentoUseCase;

        public PedidoUseCase(IPedidoRepository repository, IProdutosRepository produtoRepository, IClienteRepository clienteRepository, IMapper mapper, IPagamentoUseCase pagamentoUseCase)
        {
            _repository = repository;
            _produtoRepository = produtoRepository;
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _pagamentoUseCase = pagamentoUseCase;
        }

        public async Task<PedidoDto> EnviarPagamento(long id)
        {
            var pedido = await _repository.ObterPorId(id);

            if (pedido is null)
                throw new Exception($"PedidoId {id} inválido");

            pedido.AtualizarStatus(StatusEnum.Recebido);

            return _mapper.Map<PedidoDto>(await _repository.Atualizar(pedido));
        }

        public async Task<PedidoDto> AtualizarStatus(long id, int status)
        {
            var pedido = await _repository.ObterPorId(id);

            if (pedido is null)
                throw new Exception($"PedidoId {id} inválido");

            if (!Enum.IsDefined(typeof(StatusEnum), status))
                throw new Exception($"Status {status} inválido");

            if (pedido.Status > (StatusEnum)status)
                throw new Exception($"Status não pode retroceder");

            pedido.AtualizarStatus((StatusEnum)status);

            return _mapper.Map<PedidoDto>(await _repository.Atualizar(pedido));
        }

        public async Task<Result<object>> Inserir(CadastrarPedidoDto pedidoDto)
        {
            Cliente cliente = null;

            if (pedidoDto.ClienteId.HasValue && pedidoDto.ClienteId.Value > 0)
            {
                cliente = await _clienteRepository.ObterPorId(pedidoDto.ClienteId.Value);

                if (cliente == null)
                    throw new Exception("Cliente inválido");
            }

            var pedidoProdutos = pedidoDto.Produtos.Select(x =>
            {
                var produto = _produtoRepository.ObterPorId(x.ProdutoId).GetAwaiter().GetResult();

                if (produto == null)
                    throw new Exception($"ProdutoId {x.ProdutoId} inválido");

                return new PedidoProduto(x.ProdutoId, x.Quantidade, x.Observacao, produto);
            }).ToList();

            var pedido = new Pedido(cliente, pedidoProdutos, pedidoDto.Viagem);
            await _repository.Inserir(pedido);

            var pagamento = await _pagamentoUseCase.GerarPagamento(pedido);            
   
            return new Result<object> { Mensagem = "Pedido cadastrado com sucesso", Dados = new { NumeroPedido = pagamento.PedidoId } };
        }

        public async Task<IEnumerable<PedidoDto>> Listar()
        {
            var listaPedidos = await _repository.ListarPedidos();

            var filtrados = listaPedidos
                                .Where(x => x.Status != StatusEnum.Finalizado)
                                .OrderByDescending(x => x.Status)
                                .ThenBy(x => x.DataCriacao)
                                .ToList();

            return _mapper.Map<IEnumerable<PedidoDto>>(filtrados);
        }

        public async Task<StatusPagamentoDto> ConsultarStatusPagamento(long id)
        {
            var pedido = await _repository.ObterPorId(id);

            if (pedido is null)
                throw new Exception($"PedidoId {id} inválido");

            return new StatusPagamentoDto() { PagamentoAprovado = (pedido.Status != StatusEnum.PagamentoPendente && pedido.Status != StatusEnum.Cancelado) };
        }
    }
}
