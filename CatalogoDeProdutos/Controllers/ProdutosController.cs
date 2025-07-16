using CatalogoDeProdutos.DTOs;
using CatalogoDeProdutos.DTOs.Mappings;
using CatalogoDeProdutos.Pagination;
using CatalogoDeProdutos.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CatalogoDeProdutos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController(IUnityOfWork uof) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> ObterTodosAsync() // OK
        {
            var produtos = await uof.ProdutoService.ObterTodosAsync();

            if (produtos == null || !produtos.Any())
            {
                return BadRequest("Nenhum produto encontrado.");
            }

            return Ok(produtos);
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters produtosParameters)
        {
            try
            {
                var produtos = await uof.ProdutoService.GetProdutosAsync(produtosParameters);

                var metadata = new
                {
                    produtos.TotalCount,
                    produtos.PageSize,
                    produtos.CurrentPage,
                    produtos.TotalPages,
                    produtos.HasNext,
                    produtos.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(produtos);
            }
            catch (Exception)
            {
                return BadRequest("Não foi possivel obter os produtos.");
            }
        }

        [HttpGet("{id:int}/categorias", Name = "ObterProdutosPorCategoria")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> ObterProdutosPorCategoriaAsync(int id) // OK
        {
            try
            {
                var produtosDaCategoria = await uof.ProdutoService.ObterProdutosPorCategoria(id);

                if (produtosDaCategoria == null || !produtosDaCategoria.Any())
                {
                    return NotFound($"Nenhum produto encontrado para a categoria {id}.");
                }

                return Ok(produtosDaCategoria);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao obter produtos por categoria!");
            }
        }


        [HttpGet("{id:int}", Name = "ObterProdutoPorId")] // OK
        public async Task<ActionResult<ProdutoDTO>> ObterPorIdAsync(int id)
        {
            var produto = await uof.ProdutoService.ObterPorIdAsync(id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> AdicionarAsync([FromBody] ProdutoDTO produto) // OK  
        {
            try
            {
                var produtoEntity = await uof.ProdutoService.AdicionarAsync(produto);
                uof.Commit();

                var produtoCriado = produtoEntity.ToProdutoDTO();

                return CreatedAtRoute("ObterProdutoPorId", new { id = produtoCriado.Id }, produtoCriado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro, contate o servidor. Detalhe: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Put(int id, ProdutoDTO produto) // OK
        {
            try
            {
                await uof.ProdutoService.AtualizarAsync(produto);
                uof.Commit();
                return Ok(produto);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id) // OK
        {
            try
            {
                await uof.ProdutoService.RemoverAsync(id);
                uof.Commit();
                return Ok("Produto removido com sucesso.");
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}