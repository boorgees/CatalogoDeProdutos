using Microsoft.AspNetCore.Mvc;
using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Services.Interfaces;

namespace CatalogoDeProdutos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
    
        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> ObterTodosAsync()
        {
            var produtos = await _produtoService.ObterTodosAsync();

            if (produtos is null)
            {
                return NotFound("Nenhum produto encontrado.");
            }

            return Ok(produtos);
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Produto>> ObterPorIdAsync(int id)
        {
            var produto = await _produtoService.ObterPorIdAsync(id);
    
            if (produto is null || produto.Id == 0)
            {
                return BadRequest("Insira um ID válido para procurar o produto.");
            }

            try
            {
                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro, contate o servidor.");
            }
        }
    }
}