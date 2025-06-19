using CatalogoDeProdutos.Models;
using CatalogoDeProdutos.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoDeProdutos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController(IUnityOfWork uof) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> ObterTodosAsync()
        {
            var produtos = await uof.ProdutoService.ObterTodosAsync();

            if (produtos == null || !produtos.Any())
            {
                return NotFound("Nenhum produto encontrado.");
            }

            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name = "ObterProdutoPorId")]
        public async Task<ActionResult<Produto>> ObterPorIdAsync(int id)
        {
            var produto = await uof.ProdutoService.ObterPorIdAsync(id);

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
                await uof.ProdutoService.AdicionarAsync(produto);
                uof.Commit();


                var produtoCriado = await uof.ProdutoService.ObterPorIdAsync(produto.Id);

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
                await uof.ProdutoService.AtualizarAsync(produto);
                uof.Commit();
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
                await uof.ProdutoService.RemoverAsync(id);
                uof.Commit();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro, contate o servidor.");
            }
        }
    }
}