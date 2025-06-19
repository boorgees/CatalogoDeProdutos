using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Repositories.Interfaces;
using CatalogoDeProdutos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoDeProdutos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnityOfWork _uof;

        public ProdutosController(IProdutoService produtoService, IUnityOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> ObterTodosAsync()
        {
            var produtos = await _uof.ProdutoService.ObterTodosAsync();

            if (produtos == null || !produtos.Any())
            {
                return NotFound("Nenhum produto encontrado.");
            }

            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name = "ObterProdutoPorId")]
        public async Task<ActionResult<Produto>> ObterPorIdAsync(int id)
        {
            var produto = await _uof.ProdutoService.ObterPorIdAsync(id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> AdicionarAsync([FromBody] Produto produto)
        {
            try
            {
                await _uof.ProdutoService.AdicionarAsync(produto);
                _uof.Commit();

                // Recarrega o produto para garantir que o Id foi preenchido
                var produtoCriado = await _uof.ProdutoService.ObterPorIdAsync(produto.Id);

                return CreatedAtRoute("ObterProdutoPorId", new { id = produtoCriado.Id }, produtoCriado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro, contate o servidor. Detalhe: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Produto>> Put(int id, Produto produto)
        {
            try
            {
                await _uof.ProdutoService.AtualizarAsync(produto);
                _uof.Commit();
                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro, contate o servidor.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Produto>> Delete(int id)
        {
            try
            {
                await _uof.ProdutoService.RemoverAsync(id);
                _uof.Commit();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro, contate o servidor.");
            }
        }
    }
}