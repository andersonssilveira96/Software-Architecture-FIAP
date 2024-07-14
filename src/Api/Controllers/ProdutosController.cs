using Application.DTOs.Produtos;
using Application.UseCase.Produtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosUseCase _produtosUseCase;

        public ProdutosController(IProdutosUseCase produtosUseCase)
        {
            _produtosUseCase = produtosUseCase;
        }
        
        [HttpGet]
        public async Task<IActionResult> ListarProdutos()
        {
            var produtos = await _produtosUseCase.Listar();

            return Ok(produtos);
        }

        
        [HttpGet("categoria-produtos/{id}")]
        public async Task<IActionResult> ListarProdutosId(long id)
        {
            var produtosCategoria = await _produtosUseCase.ListarPorCategoria(id);

            return Ok(produtosCategoria);
        }
        
        [HttpPost]
        public async Task<IActionResult> Cadastrar(CadastrarProdutoDto produto)
        {
            try
            {
                await _produtosUseCase.Cadastrar(produto);

                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex);
            }          
        }
        
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromBody] AtualizarProdutoDto produto)
        {
            try
            {
                await _produtosUseCase.Atualizar(produto);

                return Ok();
            }             
            catch (Exception ex) 
            {
                return BadRequest(ex);
            } 
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(long id)
        {
            try
            {
                await _produtosUseCase.Excluir(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }           
        }
    }
}